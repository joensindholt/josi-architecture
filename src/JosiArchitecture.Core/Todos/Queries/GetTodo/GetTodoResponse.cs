namespace JosiArchitecture.Core.Todos.Queries.GetTodo
{
    public class GetTodoResponse
    {
        public GetTodoResponse(Todo todo)
        {
            Todo = new TodoResponse(todo);
        }

        public TodoResponse Todo { get; }

        public class TodoResponse
        {
            public TodoResponse(Todo todo)
            {
                Id = todo.Id;
                Title = todo.Title;
            }

            public long Id { get; }

            public string Title { get; }
        }
    }
}