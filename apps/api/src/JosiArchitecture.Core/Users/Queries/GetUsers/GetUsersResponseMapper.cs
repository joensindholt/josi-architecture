using JosiArchitecture.Core.Search;

namespace JosiArchitecture.Core.Users.Queries.GetUsers;

public static class GetUsersResponseMapper
{
    public static GetUsersResponse MapToGetUsersResponse(this IEnumerable<SearchableUser> users)
    {
        return new GetUsersResponse
        {
            Users = users.Select(u => new GetUsersResponse.User
            {
                Id = u.Id,
                Name = u.Name
            })
        };
    }
}
