using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Contacts
{
    public interface IContactRepository
    {
        Task AddAsync(Contact contact, CancellationToken cancellationToken);
    }
}