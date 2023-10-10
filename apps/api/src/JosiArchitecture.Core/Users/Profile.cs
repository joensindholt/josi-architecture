// ReSharper disable UnassignedGetOnlyAutoProperty

using JosiArchitecture.Core.Shared.Model;
using JosiArchitecture.Core.Users.Queries.GetUser;

namespace JosiArchitecture.Core.Users
{
    public class Profile : IEntity
    {
        public Profile(string title)
        {
            Title = title;
        }

        public int Id { get; private set; }

        public string Title { get; private set; }

        public static GetUserResponse.Profile FromProfile(Profile profile)
        {
            throw new System.NotImplementedException();
        }
    }
}
