using JosiArchitecture.Core.Shared.Model;
using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Shared.Persistence
{
    /// <summary>
    /// The IQueryDataStore is used by commands
    /// </summary>
    public interface ICommandDataStore
    {
        Task<T> AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class, IAggregateRoot;

        Task RemoveByIdAsync<T>(long id, CancellationToken cancellationToken) where T : class, IAggregateRoot;
    }
}
