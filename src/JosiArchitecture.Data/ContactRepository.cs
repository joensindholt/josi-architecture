using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Core.Contacts;

namespace JosiArchitecture.Data
{
    public class ContactRepository : IContactRepository
    {
        // In memory data store
        private readonly List<Contact> contacts = new List<Contact>();

        public async Task AddAsync(Contact contact, CancellationToken cancellationToken)
        {
            contacts.Add(contact);

            // Dummy await
            await Task.FromResult(0);
        }
    }
}