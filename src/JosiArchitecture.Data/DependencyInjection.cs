using JosiArchitecture.Core.Shared.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JosiArchitecture.Data
{
    public static class DependencyInjection
    {
        public static void AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataStore>(options =>
                options.UseSqlServer(configuration.GetConnectionString("JosiArchitectureDatabase")));

            services.AddScoped<IApplicationDbContext>(s => s.GetRequiredService<DataStore>());
        }
    }
}