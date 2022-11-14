using JosiArchitecture.Core.Shared.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Todos.Commands.AddTodoList
{
    public class AddTodoListHandler : AsyncRequestHandler<AddTodoListCommand>
    {
        private readonly ICommandDataStore _store;

        public AddTodoListHandler(ICommandDataStore store)
        {
            _store = store;
        }

        protected override async Task Handle(AddTodoListCommand request, CancellationToken cancellationToken)
        {
            var todoList = new TodoList(request.Title);
            await _store.AddAsync(todoList, cancellationToken);
        }
    }
}
