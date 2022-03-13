using JosiArchitecture.Core.Shared.Cqs;
using JosiArchitecture.Core.Shared.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Todos.Queries.GetTodoLists
{
    public class GetTodoListsHandler : IQueryHandler<GetTodoListsRequest, GetTodoListsResponse>
    {
        private readonly IQueryDataStore _dataStore;

        public GetTodoListsHandler(IQueryDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<GetTodoListsResponse> Handle(GetTodoListsRequest request, CancellationToken cancellationToken)
        {
            var response = new GetTodoListsResponse(_dataStore.TodoLists);
            return await Task.FromResult(response);
        }
    }
}