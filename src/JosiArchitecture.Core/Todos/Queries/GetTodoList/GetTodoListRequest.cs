using MediatR;

namespace JosiArchitecture.Core.Todos.Queries.GetTodoList
{
    public class GetTodoListRequest : IRequest<GetTodoListResponse>
    {
        public GetTodoListRequest(int id)
        {
            TodoListId = id;
        }

        public int TodoListId { get; set; }
    }
}
