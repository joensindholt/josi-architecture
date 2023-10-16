namespace JosiArchitecture.Core.Users.Queries.GetUsers;

public class GetUsersResponse
{
    public required IEnumerable<User> Users { get; init; }

    public class User
    {
        public required string Id { get; init; }

        public required string Name { get; init; }
    }
}
