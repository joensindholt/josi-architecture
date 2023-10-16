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

namespace JosiArchitecture.Api
{
    public class Program
    {
        const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

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

            // Application data services
            builder.Services.AddDataServices(builder.Configuration);

            // Api services
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

            // Other services
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

            app.MapControllers();

            app.Run();
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
