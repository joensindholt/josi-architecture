using Azure.Identity;
using JosiArchitecture.Api.Shared.ErrorHandling;
using JosiArchitecture.Core;
using JosiArchitecture.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices;
using System;
using JosiArchitecture.Api.Shared.DatabaseMigration;
using JosiArchitecture.Core.Search;
using JosiArchitecture.ElasticSearch;
using Microsoft.Extensions.Options;
using Serilog;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;
using System.IO;
using System.Text;

namespace JosiArchitecture.Api
{
    public static class Program
    {
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureLogging(builder);

            // Add Azure Key Vault to configuration
            if (builder.Environment.IsProduction())
            {
                builder.Configuration.AddAzureKeyVault(
                    new Uri($"https://josi-arch-key-vault.vault.azure.net/"),
                    new DefaultAzureCredential());
            }

            builder.Services.Configure<ElasticSearchServiceOptions>(options => builder.Configuration.GetSection("ElasticSearchService").Bind(options));

            // Application core services
            builder.Services.AddCoreServices();

            builder.Services
                .AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("Postgres")!)
                .AddElasticsearch(builder.Configuration["ElasticSearchService:Uri"]!);

            builder.Services.AddDataServices(builder.Configuration);

            builder.Services.AddApiServices();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            builder.Services.AddSingleton<ISearchService>(provider =>
            {
                var options = provider.GetRequiredService<IOptions<ElasticSearchServiceOptions>>();
                var searchProvider = new ElasticSearchService(options);
                searchProvider.Initialize();
                return searchProvider;
            });

            builder.Services.AddHostedService<DatabaseMigrator>();

            // Build the app
            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();
            app.UseApplicationErrorHandling();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();
            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = WriteResponse
            });
            app.MapControllers();
            app.Run();
        }

        private static Task WriteResponse(HttpContext context, HealthReport healthReport)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            var options = new JsonWriterOptions { Indented = true };

            using var memoryStream = new MemoryStream();
            using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
            {
                jsonWriter.WriteStartObject();
                jsonWriter.WriteString("status", healthReport.Status.ToString());
                jsonWriter.WriteStartObject("results");

                foreach (var healthReportEntry in healthReport.Entries)
                {
                    jsonWriter.WriteStartObject(healthReportEntry.Key);
                    jsonWriter.WriteString("status",
                        healthReportEntry.Value.Status.ToString());
                    jsonWriter.WriteString("description",
                        healthReportEntry.Value.Description);
                    jsonWriter.WriteStartObject("data");

                    foreach (var item in healthReportEntry.Value.Data)
                    {
                        jsonWriter.WritePropertyName(item.Key);

                        JsonSerializer.Serialize(jsonWriter, item.Value,
                            item.Value?.GetType() ?? typeof(object));
                    }

                    jsonWriter.WriteEndObject();
                    jsonWriter.WriteEndObject();
                }

                jsonWriter.WriteEndObject();
                jsonWriter.WriteEndObject();
            }

            return context.Response.WriteAsync(Encoding.UTF8.GetString(memoryStream.ToArray()));
        }

        private static void ConfigureLogging(WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog();

            if (builder.Environment.IsProduction())
            {
                builder.Logging.AddAzureWebAppDiagnostics();
                builder.Services.Configure((Action<AzureFileLoggerOptions>)(options =>
                {
                    options.FileName = "api-log";
                    options.FileSizeLimit = 50 * 1024;
                    options.RetainedFileCountLimit = 10;
                }));
            }
        }
    }
}
