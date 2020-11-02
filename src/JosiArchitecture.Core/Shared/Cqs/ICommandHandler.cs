using MediatR;

namespace JosiArchitecture.Core.Shared.Cqs
{
    public interface ICommandHandler<T> : IRequestHandler<T>
        where T : IRequest
    {
    }
    public interface ICommandHandler<T, T2> : IRequestHandler<T, T2>
        where T : IRequest<T2>
    {
    }
}