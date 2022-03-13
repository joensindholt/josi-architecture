namespace JosiArchitecture.Core.Todos.Commands.AddTodo
{
    public class AddTodoResponse
    {
        public AddTodoResponse(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}