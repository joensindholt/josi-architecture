using System.Collections.Generic;
using System.Linq;

namespace JosiArchitecture.Core.Todos.Queries.GetTodos
{
    public class GetTodosResponse
    {
        public GetTodosResponse(IEnumerable<Todos.Todo> todos)
        {
            Todos = todos.Select(todo => new Todo(todo)).ToList();
        }

        public List<Todo> Todos { get; set; }

        public class Todo
        {
            public Todo(Todos.Todo todo)
            {
                Id = todo.Id;
                Title = todo.Title;
            }

            public long Id { get; }

            public string Title { get; }
        }
    }
}