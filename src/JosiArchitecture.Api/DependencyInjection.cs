using System.Reflection;
using JosiArchitecture.Api.Shared;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

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
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.AddSwaggerGen();
        }
    }
}