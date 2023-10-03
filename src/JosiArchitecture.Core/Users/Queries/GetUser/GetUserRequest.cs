// ReSharper disable UnusedType.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

using JosiArchitecture.Core.Shared.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Users.Queries.GetUser
{
    public class GetUserRequest : IRequest<GetUserResponse?>
    {
        public int Id { get; set; }

        public GetUserRequest(int id)
        {
            Id = id;
        }
    }

    public class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse?>
    {
        private readonly IApplicationDbContext _db;

        public GetUserHandler(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<GetUserResponse?> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _db.Users
                .Where(l => l.Id == request.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            return user == default ? null : GetUserResponse.FromUser(user);
        }
    }
}