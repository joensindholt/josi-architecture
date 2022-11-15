using static JosiArchitecture.Core.Todos.Queries.GetTodoLists.GetTodoListsResponse;

namespace JosiArchitecture.Core.Todos.Queries.GetTodoLists
{
    public static class GetTodoListsResponseMapper
    {
        public static GetTodoListsResponse MapToGetTodoListsResponse(this List<TodoList> list)
        {
            return new GetTodoListsResponse(list.MapToGetTodoListsResponseItems());
        }

        public static List<GetTodoListsResponseItem> MapToGetTodoListsResponseItems(this List<TodoList> list)
        {
            return list.Select(l => new GetTodoListsResponseItem(l.Id, l.Title)).ToList();
        }
    }
}