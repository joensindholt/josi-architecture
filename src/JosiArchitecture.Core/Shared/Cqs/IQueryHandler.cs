using MediatR;

namespace JosiArchitecture.Core.Shared.Cqs
{
    public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
    }
}