// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace JosiArchitecture.Core.Users.Queries.GetUser;

public static class GetUserResponseMapper
{
    public static GetUserResponse MapToGetUserResponse(this User user)
    {
        return new GetUserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Profiles = user.Profiles.Select(p => new GetUserResponse.Profile
            {
                Id = p.Id,
                Title = p.Title
            })
        };
    }
}
