// ReSharper disable UnusedType.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

using JosiArchitecture.Core.Shared.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using OneOf;
using OneOf.Types;

namespace JosiArchitecture.Core.Users.Queries.GetUser
{
    public class GetUserRequest : IRequest<OneOf<GetUserResponse, NotFound>>
    {
        public string Id { get; set; }

        public GetUserRequest(string id)
        {
            Id = id;
        }
    }

    public class GetUserHandler : IRequestHandler<GetUserRequest, OneOf<GetUserResponse, NotFound>>
    {
        private readonly IApplicationDbContext _db;

        public GetUserHandler(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<OneOf<GetUserResponse, NotFound>> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out int userId))
            {
                return new NotFound();
            }

            var user = await _db.Users
                .Where(l => l.Id == userId)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (user == default)
            {
                return new NotFound();
            }

            return user.MapToGetUserResponse();
        }
    }
}
