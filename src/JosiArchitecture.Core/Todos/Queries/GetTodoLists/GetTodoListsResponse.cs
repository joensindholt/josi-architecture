using System.Collections.Generic;
using System.Linq;

namespace JosiArchitecture.Core.Todos.Queries.GetTodoLists
{
    public class GetTodoListsResponse
    {
        public GetTodoListsResponse(IEnumerable<TodoList> todoLists)
        {
            TodoLists = todoLists.Select(todoList => new GetTodoListsResponseItem(todoList)).ToList();
        }

        public List<GetTodoListsResponseItem> TodoLists { get; set; }

        public class GetTodoListsResponseItem
        {
            public GetTodoListsResponseItem(TodoList todoList)
            {
                Id = todoList.Id;
                Title = todoList.Title;
            }

            public long Id { get; }

            public string Title { get; }
        }
    }
}
