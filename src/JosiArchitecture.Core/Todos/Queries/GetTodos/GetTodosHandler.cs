using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Core.Shared;
using JosiArchitecture.Core.Shared.Cqs;

namespace JosiArchitecture.Core.Todos.Queries.GetTodos
{
    public class GetTodosHandler : IQueryHandler<GetTodosRequest, GetTodosResponse>
    {
        private readonly IQueryDataStore _dataStore;

        public GetTodosHandler(IQueryDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<GetTodosResponse> Handle(GetTodosRequest request, CancellationToken cancellationToken)
        {
            var response = new GetTodosResponse(_dataStore.Todos);
            return await Task.FromResult(response);
        }
    }
}