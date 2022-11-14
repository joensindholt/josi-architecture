using JosiArchitecture.Core.Shared.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Todos.Commands.RemoveTodo
{
    public class RemoveTodoHandler : AsyncRequestHandler<RemoveTodoCommand>
    {
        private readonly IQueryDataStore _store;

        public RemoveTodoHandler(IQueryDataStore store)
        {
            _store = store;
        }

        protected override async Task Handle(RemoveTodoCommand request, CancellationToken cancellationToken)
        {
            var todoList = await _store.TodoLists
                .Include(l => l.Todos)
                .SingleAsync(l => l.Id == request.TodoListId);

            todoList.RemoveTodo(request.Id);
        }
    }
}
