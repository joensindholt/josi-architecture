using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Core.Shared.Cqs;

namespace JosiArchitecture.Core.Todos.Commands
{
    public class RemoveTodoHandler : ICommandHandler<RemoveTodoCommand>
    {
        private readonly ITodoRepository todoRepository;

        public RemoveTodoHandler(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        public async Task Handle(RemoveTodoCommand request, CancellationToken cancellationToken)
        {
            await todoRepository.RemoveByIdAsync(request.Id, cancellationToken);
        }
    }

    public class RemoveTodoCommand : ICommand
    {
        public long Id { get; set; }
    }
}