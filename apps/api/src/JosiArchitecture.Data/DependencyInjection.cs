using System;
using System.Reflection;
using JosiArchitecture.Core.Shared.Persistence;
using JosiArchitecture.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JosiArchitecture.Data
{
    public static class DependencyInjection
    {
        public static void AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddDatabase(services, configuration);
            services.Configure<DatabaseOptions>(options => configuration.GetSection("Database").Bind(options));
            services.AddScoped<IApplicationDbContext>(s => s.GetRequiredService<DataStore>());
        }

        private static void AddDatabase(IServiceCollection services, IConfiguration configuration)
        {
            // Read database options directly from configuration
            var databaseOptions = new DatabaseOptions();
            configuration.GetSection("Database").Bind(databaseOptions);

            switch (databaseOptions.Provider)
            {
                case DatabaseOptions.DatabaseProvider.SqlServer:
                    services.AddDbContext<DataStore>(options => options.UseSqlServer(configuration.GetConnectionString("JosiArchitectureDatabase")));
                    break;
                case DatabaseOptions.DatabaseProvider.Postgres:
                    services.AddDbContext<DataStore>(options => options.UseNpgsql(configuration.GetConnectionString("Postgres")));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(databaseOptions), $"Unhandled provider");
            }
        }
    }
}
