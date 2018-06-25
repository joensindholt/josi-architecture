using MediatR;

namespace JosiArchitecture.Core.Shared.Cqs
{
    public interface ICommandHandler<T> : IRequestHandler<T>
        where T : IRequest
    {
    }
}