using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JosiArchitecture.Api
{
    public static class DependencyInjection
    {
        public static void AddApiServices(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddLogging();
            services.AddHttpContextAccessor();
        }
    }
}
