// ReSharper disable UnusedType.Global

using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Core.Search;
using JosiArchitecture.Core.Shared.Persistence;
using MediatR;
using OneOf;
using OneOf.Types;

namespace JosiArchitecture.Core.Users.Commands.DeleteUser;

public class DeleteUserRequest : IRequest<OneOf<DeleteUserResponse, NotFound>>
{
    public DeleteUserRequest(string id)
    {
        Id = id;
    }

    public string Id { get; private set; }
}

public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, OneOf<DeleteUserResponse, NotFound>>
{
    private readonly IApplicationDbContext _db;
    private readonly ISearchService _searchService;

    public DeleteUserHandler(IApplicationDbContext db, ISearchService searchService)
    {
        _db = db;
        _searchService = searchService;
    }

    public async Task<OneOf<DeleteUserResponse, NotFound>> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        if (!int.TryParse(request.Id, out int userId))
        {
            return new NotFound();
        }

        var user = await _db.Users.FindAsync(userId);

        if (user is null)
        {
            return new NotFound();
        }

        _db.Users.Remove(user);
        await _db.SaveChangesAsync(cancellationToken);

        await _searchService.RemoveAsync(request.Id, cancellationToken);

        return new DeleteUserResponse
        {
            Id = user.Id!.ToString()
        };
    }
}
