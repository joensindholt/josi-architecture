// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace JosiArchitecture.Core.Users.Queries.GetUser;

public class GetUserResponse
{
    public required long Id { get; init; }

    public required string Name { get; init; }

    public required IEnumerable<Profile> Profiles { get; init; }

    public class Profile
    {
        public required int Id { get; init; }

        public required string Title { get; init; }
    }
}
