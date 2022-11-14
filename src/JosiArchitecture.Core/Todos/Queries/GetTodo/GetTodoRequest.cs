using MediatR;

namespace JosiArchitecture.Core.Todos.Queries.GetTodo
{
    public class GetTodoRequest : IRequest<GetTodoResponse>
    {
        public GetTodoRequest(long todoListId, long id)
        {
            TodoListId = todoListId;
            Id = id;
        }

        public long Id { get; }

        public long TodoListId { get; set; }
    }
}
