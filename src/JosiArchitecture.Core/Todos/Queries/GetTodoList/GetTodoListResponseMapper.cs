using static JosiArchitecture.Core.Todos.Queries.GetTodoList.GetTodoListResponse;

namespace JosiArchitecture.Core.Todos.Queries.GetTodoList
{
    public static class GetTodoListResponseMapper
    {
        public static GetTodoListResponse MapToGetTodoListResponse(this TodoList list)
        {
            return new GetTodoListResponse(list.Id, list.Title, list.Todos.MapToGetTodoListsResponseItems());
        }

        public static List<GetTodoListResponseItem> MapToGetTodoListsResponseItems(this List<Todo> todos)
        {
            return todos.Select(l => new GetTodoListResponseItem(l.Id, l.Title)).ToList();
        }
    }
}