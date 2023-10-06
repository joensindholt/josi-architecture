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
        public int Id { get; set; }

        public GetUserRequest(int id)
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
            var user = await _db.Users
                .Where(l => l.Id == request.Id)
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
