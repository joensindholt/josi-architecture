using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Core.Shared;
using JosiArchitecture.Core.Shared.Cqs;
using Microsoft.EntityFrameworkCore;

namespace JosiArchitecture.Core.Todos.Queries
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

    public class GetTodoListRequest : IQuery<GetTodoListResponse>
    {
        public GetTodoListRequest(int id)
        {
            TodoListId = id;
        }

        public int TodoListId { get; set; }
    }

    public class GetTodoListResponse
    {
        public GetTodoListResponse(IEnumerable<TodoList> todoLists)
        {
            TodoLists = todoLists.Select(todoList => new GetTodoListResponseItem(todoList)).ToList();
        }

        public List<GetTodoListResponseItem> TodoLists { get; set; }

        public class GetTodoListResponseItem
        {
            public GetTodoListResponseItem(TodoList todoList)
            {
                Id = todoList.Id;
                Title = todoList.Title;
            }

            public long Id { get; }

            public string Title { get; }
        }
    }
}
