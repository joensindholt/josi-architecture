using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Core.Shared;
using JosiArchitecture.Core.Shared.Cqs;

namespace JosiArchitecture.Core.Todos.Commands.AddTodo
{
    public class AddTodoHandler : ICommandHandler<AddTodoCommand, AddTodoResponse>
    {
        private readonly ICommandDataStore _store;

        public AddTodoHandler(ICommandDataStore store)
        {
            _store = store;
        }

        public async Task<AddTodoResponse> Handle(AddTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = new Todo(request.Title);
            await _store.AddAsync(todo, cancellationToken);
            return new AddTodoResponse(todo.Id);
        }
    }
}