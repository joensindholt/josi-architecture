using JosiArchitecture.Core.Shared.Cqs;

namespace JosiArchitecture.Core.Todos.Commands.AddTodoList
{
    public class AddTodoListCommand : ICommand
    {
        public string Title { get; set; }
    }
}
