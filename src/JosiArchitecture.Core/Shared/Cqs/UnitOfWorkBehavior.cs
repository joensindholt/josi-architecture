using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JosiArchitecture.Core.Shared.Cqs
{
    public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UnitOfWorkBehavior<TRequest, TResponse>> _logger;

        public UnitOfWorkBehavior(
            IUnitOfWork unitOfWork,
            ILogger<UnitOfWorkBehavior<TRequest, TResponse>> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();

            if (request is ICommand)
            {
                await _unitOfWork.CompleteAsync(cancellationToken);

                _logger.LogInformation("Unit of work completed");
            }

            return response;
        }
    }
}