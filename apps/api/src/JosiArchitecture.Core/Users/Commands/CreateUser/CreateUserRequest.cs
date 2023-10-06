// ReSharper disable UnusedType.Global

using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Core.Search;
using JosiArchitecture.Core.Shared.Persistence;
using MediatR;

namespace JosiArchitecture.Core.Users.Commands.CreateUser;

public class CreateUserRequest : IRequest<int>
{
    public string? Name { get; set; }
}

public class CreateUserHandler : IRequestHandler<CreateUserRequest, int>
{
    private readonly IApplicationDbContext _db;
    private readonly ISearchService _searchService;

    public CreateUserHandler(IApplicationDbContext db, ISearchService searchService)
    {
        _db = db;
        _searchService = searchService;
    }

    public async Task<int> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = new User(request.Name!);

        await _db.Users.AddAsync(user, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        await _searchService.AddAsync(SearchableUser.FromUser(user), cancellationToken);

        return user.Id;
    }
}