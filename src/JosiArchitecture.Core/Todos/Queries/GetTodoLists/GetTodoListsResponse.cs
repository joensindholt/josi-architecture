using System.Collections.Generic;

namespace JosiArchitecture.Core.Todos.Queries.GetTodoLists
{
    public class GetTodoListsResponse
    {
        public GetTodoListsResponse(List<GetTodoListsResponseItem> todoLists)
        {
            TodoLists = todoLists;
        }

        public List<GetTodoListsResponseItem> TodoLists { get; }

        public class GetTodoListsResponseItem
        {
            public GetTodoListsResponseItem(long id, string title)
            {
                Id = id;
                Title = title;
            }

            public long Id { get; }

            public string Title { get; }
        }
    }
}