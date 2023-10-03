using JosiArchitecture.Core.Shared.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using JosiArchitecture.Core.Search;

namespace JosiArchitecture.Core.Users.Queries.GetUsers;

public class GetUsersRequest : IRequest<GetUsersResponse>
{
    public string? OrderBy { get; set; }
}

public class GetUsersHandler : IRequestHandler<GetUsersRequest, GetUsersResponse>
{
    private readonly IApplicationDbContext _db;
    private readonly ISearchService _searchService;

    public GetUsersHandler(IApplicationDbContext db, ISearchService searchService)
    {
        _db = db;
        _searchService = searchService;
    }

    public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await _searchService.QueryUsersAsync(request.OrderBy, cancellationToken);
        var response = new GetUsersResponse(users.Select(u => new GetUsersResponse.User(u.Id, u.Name)));
        return await Task.FromResult(response);
    }
}