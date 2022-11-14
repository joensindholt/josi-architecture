using JosiArchitecture.Core.Shared.Cqs;
using JosiArchitecture.Core.Shared.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Todos.Queries.GetTodoList
{
    public class GetTodoListHandler : IQueryHandler<GetTodoListRequest, GetTodoListResponse>
    {
        private readonly IQueryDataStore _dataStore;

        public GetTodoListHandler(IQueryDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<GetTodoListResponse> Handle(GetTodoListRequest request, CancellationToken cancellationToken)
        {
            var todoLists = await _dataStore.TodoLists.Where(l => l.Id == request.TodoListId).ToListAsync();
            return new GetTodoListResponse(todoLists);
        }
    }
}
