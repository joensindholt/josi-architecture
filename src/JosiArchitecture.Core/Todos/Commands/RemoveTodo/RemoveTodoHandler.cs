using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Core.Shared;
using JosiArchitecture.Core.Shared.Cqs;

namespace JosiArchitecture.Core.Todos.Commands.RemoveTodo
{
    public class RemoveTodoHandler : ICommandHandler<RemoveTodoCommand>
    {
        private readonly ICommandDataStore _store;

        public RemoveTodoHandler(ICommandDataStore store)
        {
            _store = store;
        }

        public async Task Handle(RemoveTodoCommand request, CancellationToken cancellationToken)
        {
            await _store.RemoveByIdAsync<Todo>(request.Id, cancellationToken);
        }
    }
}