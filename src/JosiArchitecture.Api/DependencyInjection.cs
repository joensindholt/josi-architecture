using JosiArchitecture.Api.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JosiArchitecture.Api
{
    public static class DependencyInjection
    {
        public static void AddApiServices(this IServiceCollection services, params Assembly[] assemblies)
        {
            services
                .AddControllers(options =>
                {
                    options.Filters.Add(new ModelstateValidationFilter());
                    options.Filters.Add(new ExceptionFilter());
                });

            services.AddSwaggerGen();
        }
    }
}
