using System.Collections.Generic;
using System.Linq;

namespace JosiArchitecture.Core.Todos.Queries.GetTodoList
{
    public class GetTodoListResponse
    {
        public GetTodoListResponse(IEnumerable<TodoList> todoLists)
        {
            TodoLists = todoLists.Select(todoList => new GetTodoListResponseItem(todoList)).ToList();
        }

        public List<GetTodoListResponseItem> TodoLists { get; set; }

        public class GetTodoListResponseItem
        {
            public GetTodoListResponseItem(TodoList todoList)
            {
                Id = todoList.Id;
                Title = todoList.Title;
            }

            public long Id { get; }

            public string Title { get; }
        }
    }
}
