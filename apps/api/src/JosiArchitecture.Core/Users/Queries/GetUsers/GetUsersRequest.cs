// ReSharper disable UnusedType.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Core.Search;

namespace JosiArchitecture.Core.Users.Queries.GetUsers;

public class GetUsersRequest : IRequest<GetUsersResponse>
{
    public string? OrderBy { get; set; }
}

public class GetUsersHandler : IRequestHandler<GetUsersRequest, GetUsersResponse>
{
    private readonly ISearchService _searchService;

    public GetUsersHandler(ISearchService searchService)
    {
        _searchService = searchService;
    }

    public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await _searchService.QueryUsersAsync(request.OrderBy, cancellationToken);
        var response = users.MapToGetUsersResponse();
        return await Task.FromResult(response);
    }
}
