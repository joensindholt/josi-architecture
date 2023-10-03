// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace JosiArchitecture.Core.Users.Queries.GetUser;

public class GetUserResponse
{
    public long Id { get; init; }

    public string Name { get; init; } = null!;

    public IEnumerable<Profile> Profiles { get; init; } = new List<Profile>();

    public class Profile
    {
        public int Id { get; init; }

        public string Title { get; init; } = null!;

        public static Profile FromProfile(Users.Profile profile)
        {
            return new Profile
            {
                Id = profile.Id,
                Title = profile.Title
            };
        }
    }

    public static GetUserResponse FromUser(User user)
    {
        return new GetUserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Profiles = user.Profiles.Select(Profile.FromProfile)
        };
    }
}