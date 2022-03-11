using JosiArchitecture.Core.Shared.Cqs;

namespace JosiArchitecture.Core.Todos.Commands.AddTodo
{
    public class AddTodoCommand : ICommand<AddTodoResponse>
    {
        public string Title { get; set; }
    }
}