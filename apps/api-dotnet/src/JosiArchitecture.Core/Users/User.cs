// ReSharper disable UnassignedGetOnlyAutoProperty

using JosiArchitecture.Core.Shared.Model;

namespace JosiArchitecture.Core.Users
{
    public class User : AggregateRoot
    {
        public User(string name)
        {
            Name = name;
            _profiles = new List<Profile>();
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        private readonly List<Profile> _profiles;

        public IEnumerable<Profile> Profiles => _profiles;

        public void AddProfile(Profile profile)
        {
            _profiles.Add(profile);
        }

        public void RemoveProfile(long id)
        {
            _profiles.RemoveAll(p => p.Id == id);
        }
    }
}
