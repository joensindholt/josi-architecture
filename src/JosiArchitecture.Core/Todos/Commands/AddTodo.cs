using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Core.Shared.Cqs;

namespace JosiArchitecture.Core.Todos.Commands
{
    public class AddTodoHandler : ICommandHandler<AddTodoCommand>
    {
        private readonly ITodoRepository _todoRepository;

        public AddTodoHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task Handle(AddTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = new Todo(request.Title);
            await _todoRepository.AddAsync(todo, cancellationToken);
        }
    }

    public class AddTodoCommand : ICommand
    {
        public string Title { get; set; }
    }
}