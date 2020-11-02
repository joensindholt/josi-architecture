using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Core.Shared;
using JosiArchitecture.Core.Shared.Cqs;

namespace JosiArchitecture.Core.Todos.Commands
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
            return await _store.AddAsync(todo, cancellationToken);
        }
    }

    public class AddTodoCommand : ICommand<AddTodoResponse>
    {
        public string Title { get; set; }
    }

    public class AddTodoResponse
    {

    }
}