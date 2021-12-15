using JosiArchitecture.Core.Shared;
using JosiArchitecture.Core.Shared.Cqs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Todos.Queries
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
            var todo = _dataStore.Todos.FirstOrDefault(t => t.Id == request.Id);

            if (todo == null)
            {
                return null;
            }

            var response = new GetTodoResponse(todo);

            return await Task.FromResult(response);
        }
    }

    public class GetTodoRequest : IQuery<GetTodoResponse>
    {
        public GetTodoRequest(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }

    public class GetTodoResponse
    {
        public GetTodoResponse(Todo todo)
        {
            Todo = new TodoResponse(todo);
        }

        public TodoResponse Todo { get; }

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