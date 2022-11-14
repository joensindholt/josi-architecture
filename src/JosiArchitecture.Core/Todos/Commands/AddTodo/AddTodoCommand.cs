using MediatR;

namespace JosiArchitecture.Core.Todos.Commands.AddTodo
{
    public class AddTodoCommand : IRequest<AddTodoResponse>
    {
        public long TodoListId { get; set; }

        public string Title { get; set; }
    }
}
