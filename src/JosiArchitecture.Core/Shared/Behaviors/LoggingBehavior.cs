using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Shared.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;

        public LoggingBehavior(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation($"Handling {typeof(TRequest).Name}");

            try
            {
                TResponse response = await next();

                _logger.LogInformation($"Handled {typeof(TRequest).Name}");

                return response;
            }
            catch (Exception ex)
            {
                if (ExceptionShouldBeLogged(ex))
                {
                    _logger.LogError(ex, $"Unhandled exception occured handling {typeof(TRequest).Name}");
                }

                throw;
            }
        }

        private bool ExceptionShouldBeLogged(Exception ex)
        {
            if (ex is ValidationException)
            {
                // ValidationException is thrown on invalid user input. This should not be logged
                return false;
            }

            return true;
        }
    }
}