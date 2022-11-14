using JosiArchitecture.Core.Shared.Cqs;
using JosiArchitecture.Core.Shared.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Todos.Queries.GetTodo
{
    public class GetTodoHandler : IQueryHandler<GetTodoRequest, GetTodoResponse>
    {
        private readonly IQueryDataStore _dataStore;

        public GetTodoHandler(IQueryDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<GetTodoResponse> Handle(GetTodoRequest request, CancellationToken cancellationToken)
        {
            var list = _dataStore.TodoLists.FirstOrDefault(l => l.Id == request.TodoListId);

            if (list == null)
            {
                return null;
            }

            var todo = list.Todos.FirstOrDefault(t => t.Id == request.Id);

            if (todo == null)
            {
                return null;
            }

            var response = new GetTodoResponse(todo);

            return await Task.FromResult(response);
        }
    }
}
