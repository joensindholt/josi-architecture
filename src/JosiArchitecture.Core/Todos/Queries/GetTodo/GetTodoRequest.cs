using JosiArchitecture.Core.Shared.Cqs;

namespace JosiArchitecture.Core.Todos.Queries.GetTodo
{
    public class GetTodoRequest : IQuery<GetTodoResponse>
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
