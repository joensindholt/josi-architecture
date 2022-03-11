using JosiArchitecture.Core.Shared.Cqs;

namespace JosiArchitecture.Core.Todos.Queries.GetTodoList
{
    public class GetTodoListRequest : IQuery<GetTodoListResponse>
    {
        public GetTodoListRequest(int id)
        {
            TodoListId = id;
        }

        public int TodoListId { get; set; }
    }
}
