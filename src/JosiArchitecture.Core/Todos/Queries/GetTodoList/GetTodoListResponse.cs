namespace JosiArchitecture.Core.Todos.Queries.GetTodoList
{
    public class GetTodoListResponse
    {
        public GetTodoListResponse(long id, string title, List<GetTodoListResponseItem> todos)
        {
            Id = id;
            Title = title;
            Todos = todos;
        }

        public long Id { get; }
        public string Title { get; }
        public List<GetTodoListResponseItem> Todos { get; }

        public class GetTodoListResponseItem
        {
            public GetTodoListResponseItem(long id, string title)
            {
                Id = id;
                Title = title;
            }

            public long Id { get; }

            public string Title { get; }
        }
    }
}