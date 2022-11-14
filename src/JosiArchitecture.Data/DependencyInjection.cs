using JosiArchitecture.Core.Shared.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace JosiArchitecture.Data
{
    public static class DependencyInjection
    {
        public static void AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<IQueryDataStore, DataStore>();
            services.AddScoped<ICommandDataStore, DataStore>();
            services.AddScoped<DataStore, DataStore>();
            services.AddScoped<IUnitOfWork, DataStore>();
        }
    }
}
