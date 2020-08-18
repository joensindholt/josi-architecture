using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Core.Shared;
using JosiArchitecture.Core.Shared.Cqs;

namespace JosiArchitecture.Core.Todos.Queries
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

    public class GetTodosRequest : IQuery<GetTodosResponse>
    {
    }

    public class GetTodosResponse
    {
        public GetTodosResponse(IEnumerable<Todo> todos)
        {
            Todos = todos.Select(todo => new TodoResponse(todo)).ToList();
        }

        public List<TodoResponse> Todos { get; set; }

        public class TodoResponse
        {
            public TodoResponse(Todo todo)
            {
                Id = todo.Id;
                Title = todo.Title;
            }

            public long Id { get; }

            public string Title { get; }
        }
    }
}