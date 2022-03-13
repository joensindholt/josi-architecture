using JosiArchitecture.Core.Shared.Cqs;

namespace JosiArchitecture.Core.Todos.Commands.AddTodo
{
    public class AddTodoCommand : ICommand<AddTodoResponse>
    {
        public long TodoListId { get; set; }

        public string Title { get; set; }
    }
}