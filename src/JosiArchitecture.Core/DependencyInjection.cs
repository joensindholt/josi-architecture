using System.Reflection;
using JosiArchitecture.Core.Shared.Cqs;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace JosiArchitecture.Core
{
    public static class DependencyInjection
    {
        public static void AddCoreServices(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMediatR(assemblies);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        }


    }
}