using JosiArchitecture.Core.Shared.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Todos.Queries.GetTodoList
{
    public class GetTodoListRequest : IRequest<GetTodoListResponse>
    {
        public GetTodoListRequest(int id)
        {
            TodoListId = id;
        }

        public int TodoListId { get; set; }
    }

    public class GetTodoListHandler : IRequestHandler<GetTodoListRequest, GetTodoListResponse>
    {
        private readonly IApplicationDbContext _db;

        public GetTodoListHandler(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<GetTodoListResponse> Handle(GetTodoListRequest request, CancellationToken cancellationToken)
        {
            var todoList = await _db.TodoLists.Where(l => l.Id == request.TodoListId).AsNoTracking().FirstOrDefaultAsync();

            if (todoList == null)
            {
                return null;
            }

            return todoList.MapToGetTodoListResponse();
        }
    }
}