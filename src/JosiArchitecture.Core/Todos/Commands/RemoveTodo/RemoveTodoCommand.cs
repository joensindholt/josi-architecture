using JosiArchitecture.Core.Shared.Cqs;

namespace JosiArchitecture.Core.Todos.Commands.RemoveTodo
{
    public class RemoveTodoCommand : ICommand
    {
        public long TodoListId { get; set; }

        public long Id { get; set; }
    }
}
