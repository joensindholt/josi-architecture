using MediatR;

namespace JosiArchitecture.Core.Shared.Cqs
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<T> : IRequest<T>
    {
    }
}