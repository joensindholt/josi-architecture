using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Core.Shared;
using JosiArchitecture.Core.Shared.Cqs;

namespace JosiArchitecture.Core.Todos.Queries
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

    public class GetTodoListsRequest : IQuery<GetTodoListsResponse>
    {
    }

    public class GetTodoListsResponse
    {
        public GetTodoListsResponse(IEnumerable<TodoList> todoLists)
        {
            TodoLists = todoLists.Select(todoList => new GetTodoListsResponseItem(todoList)).ToList();
        }

        public List<GetTodoListsResponseItem> TodoLists { get; set; }

        public class GetTodoListsResponseItem
        {
            public GetTodoListsResponseItem(TodoList todoList)
            {
                Id = todoList.Id;
                Title = todoList.Title;
            }

            public long Id { get; }

            public string Title { get; }
        }
    }
}