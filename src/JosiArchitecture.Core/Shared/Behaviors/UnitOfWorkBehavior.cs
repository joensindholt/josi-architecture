using JosiArchitecture.Core.Shared.Cqs;
using JosiArchitecture.Core.Shared.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Shared.Behaviors
{
    public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public UnitOfWorkBehavior(
            IUnitOfWork unitOfWork,
            ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();

            // TODO: Makes this check work and test it
            if (request.GetType() is ICommand || request.GetType().IsAssignableToGenericType(typeof(ICommand<>)))
            {
                await _unitOfWork.CompleteAsync(cancellationToken);

                _logger.LogInformation("Unit of work completed");
            }

            return response;
        }
    }
}
