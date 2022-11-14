using MediatR;

namespace JosiArchitecture.Core.Shared.Cqs
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
