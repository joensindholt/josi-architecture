using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Core.Shared;
using JosiArchitecture.Core.Shared.Cqs;

namespace JosiArchitecture.Core.Todos.Commands
{
    public class AddTodoHandler : ICommandHandler<AddTodoCommand>
    {
        private readonly ICommandDataStore _store;

        public AddTodoHandler(ICommandDataStore store)
        {
            _store = store;
        }

        public async Task Handle(AddTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = new Todo(request.Title);
            await _store.AddAsync(todo, cancellationToken);
        }
    }

    public class AddTodoCommand : ICommand
    {
        public string Title { get; set; }
    }
}