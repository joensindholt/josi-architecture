using JosiArchitecture.Core.Shared.Cqs;
using JosiArchitecture.Core.Shared.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Todos.Commands.AddTodo
{
    public class AddTodoHandler : ICommandHandler<AddTodoCommand, AddTodoResponse>
    {
        private readonly IQueryDataStore _store;

        public AddTodoHandler(IQueryDataStore store)
        {
            _store = store;
        }

        public async Task<AddTodoResponse> Handle(AddTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = new Todo(request.Title);

            var todoList = await _store.TodoLists
                .Include(l => l.Todos)
                .SingleAsync(l => l.Id == request.TodoListId, cancellationToken);

            todoList.AddTodo(todo);

            return new AddTodoResponse(todo.Id);
        }
    }
}
