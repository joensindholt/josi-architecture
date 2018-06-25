using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Core.Shared;
using JosiArchitecture.Core.Shared.Cqs;

namespace JosiArchitecture.Core.Todos.Queries
{
    public class GetTodosHandler : IQueryHandler<GetTodosRequest, IEnumerable<Todo>>
    {
        private readonly IQueryDataStore _dataStore;

        public GetTodosHandler(IQueryDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<IEnumerable<Todo>> Handle(GetTodosRequest request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_dataStore.Todos.ToList());
        }
    }

    public class GetTodosRequest : IQuery<IEnumerable<Todo>>
    {
    }
}