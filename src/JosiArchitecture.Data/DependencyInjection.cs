using JosiArchitecture.Core.Shared.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JosiArchitecture.Data
{
    public static class DependencyInjection
    {
        public static void AddDataServices(this IServiceCollection services)
        {
            services.AddDbContext<DataStore>(options =>
                options.UseSqlServer("Server=localhost;Database=JosiArchitecture;User Id=sa;Password=letmepass!!42;TrustServerCertificate=True"));

            services.AddScoped<IApplicationDbContext>(s => s.GetRequiredService<DataStore>());
        }
    }
}