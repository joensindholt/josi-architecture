using JosiArchitecture.Core.Shared.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Todos.Queries.GetTodoLists
{
    public class GetTodoListsRequest : IRequest<GetTodoListsResponse>
    {
    }

    public class GetTodoListsHandler : IRequestHandler<GetTodoListsRequest, GetTodoListsResponse>
    {
        private readonly IApplicationDbContext _db;

        public GetTodoListsHandler(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<GetTodoListsResponse> Handle(GetTodoListsRequest request, CancellationToken cancellationToken)
        {
            var lists = await _db.TodoLists.AsNoTracking().ToListAsync();
            var response = lists.MapToGetTodoListsResponse();
            return await Task.FromResult(response);
        }
    }
}