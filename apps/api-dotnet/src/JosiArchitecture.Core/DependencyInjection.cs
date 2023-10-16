using JosiArchitecture.Core.Shared.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using JosiArchitecture.Core.Shared.Extensions;
using FluentValidation.Internal;
using System;
using FluentValidation.Resources;
using System.Globalization;

namespace JosiArchitecture.Core
{
    public static class DependencyInjection
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            // MediatR
            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // Fluent Validation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            CamelCaseFluentValidationMembers();
        }

        private static void CamelCaseFluentValidationMembers()
        {
            ValidatorOptions.Global.MessageFormatterFactory = () => new MyMessageFormatter();
            ValidatorOptions.Global.LanguageManager = new MyLanguageManager();

            ValidatorOptions.Global.DisplayNameResolver = (type, member, expression) =>
            {
                if (member != null)
                {
                    return member.Name.CamelCase();
                }

                return null;
            };

            ValidatorOptions.Global.PropertyNameResolver = (type, member, expression) =>
            {
                if (member != null)
                {
                    return member.Name.CamelCase();
                }

                return null;
            };
        }
    }

    internal class MyLanguageManager : FluentValidation.Resources.LanguageManager
    {
        public MyLanguageManager()
        {
            AddTranslation("en", "NotEmptyValidator", "{PropertyName} should not be empty");
            AddTranslation("en-US", "NotEmptyValidator", "{PropertyName} should not be empty");
            AddTranslation("en-GB", "NotEmptyValidator", "{PropertyName} should not be empty");
        }
    }

    internal class MyMessageFormatter : MessageFormatter
    {
        public override string BuildMessage(string messageTemplate)
        {
            messageTemplate = messageTemplate.Replace("'{PropertyName}'", "{PropertyName}");
            return base.BuildMessage(messageTemplate);
        }
    }
}
