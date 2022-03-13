using FluentValidation;
using JosiArchitecture.Core.Shared.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JosiArchitecture.Core.Shared
{
    public static class DependencyInjection
    {
        public static void AddCqs(this IServiceCollection services, params Type[] assemblyMarkerTypes)
        {
            var assemblyMarkerTypeList = assemblyMarkerTypes?.ToList() ?? new List<Type>();
            assemblyMarkerTypeList.Add(typeof(CoreMarker));
            services.AddMediatR(assemblyMarkerTypeList);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

            services.AddValidatorsFromAssemblyContaining<CoreMarker>(ServiceLifetime.Scoped);
        }
    }
}