// ReSharper disable MemberCanBePrivate.Global

using JosiArchitecture.Core.Users;

namespace JosiArchitecture.Core.Search;

public class SearchableUser
{
    public string Name { get; }

    public int Id { get; }

    public SearchableUser(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public static SearchableUser FromUser(User user)
    {
        return new SearchableUser(user.Id, user.Name);
    }
}