using JosiArchitecture.Core.Shared.Cqs;

namespace JosiArchitecture.Core.Todos.Queries.GetTodo
{
    public class GetTodoRequest : IQuery<GetTodoResponse>
    {
        public GetTodoRequest(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}