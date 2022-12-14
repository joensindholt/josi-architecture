using Azure.Identity;
using JosiArchitecture.Api.Shared.ErrorHandling;
using JosiArchitecture.Core;
using JosiArchitecture.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices;
using System;

namespace JosiArchitecture.Api
{
    public class Program
    {
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

            // Application core services
            builder.Services.AddCoreServices();

            // Application data services
            builder.Services.AddDataServices(builder.Configuration);

            // Api services
            builder.Services.AddApiServices();

            var app = builder.Build();

            if (builder.Environment.IsProduction())
            {
                MigrateDatabase(app);
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseApplicationErrorHandling();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void MigrateDatabase(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var dataStore = scope.ServiceProvider.GetRequiredService<DataStore>();
            dataStore.Database.Migrate();
        }

        private static void ConfigureLogging(WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            if (builder.Environment.IsProduction())
            {
                builder.Logging.AddAzureWebAppDiagnostics();

                builder.Services.Configure((System.Action<AzureFileLoggerOptions>)(options =>
                {
                    options.FileName = "api-log";
                    options.FileSizeLimit = 50 * 1024;
                    options.RetainedFileCountLimit = 10;
                }));
            }
        }
    }
}