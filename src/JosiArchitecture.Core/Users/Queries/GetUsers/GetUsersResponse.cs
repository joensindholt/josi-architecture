namespace JosiArchitecture.Core.Users.Queries.GetUsers;

public class GetUsersResponse
{
    public GetUsersResponse(IEnumerable<User> users)
    {
        Users = users;
    }

    public IEnumerable<User> Users { get; }

    public class User
    {
        public User(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public long Id { get; }

        public string Name { get; }
    }
}