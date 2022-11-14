using MediatR;

namespace JosiArchitecture.Core.Todos.Commands.AddTodoList
{
    public class AddTodoListCommand : IRequest
    {
        public string Title { get; set; }
    }
}
