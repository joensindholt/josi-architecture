using MediatR;

namespace JosiArchitecture.Core.Todos.Commands.RemoveTodo
{
    public class RemoveTodoCommand : IRequest
    {
        public long TodoListId { get; set; }

        public long Id { get; set; }
    }
}
