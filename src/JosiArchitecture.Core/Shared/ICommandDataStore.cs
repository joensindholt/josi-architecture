using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Shared
{
    /// <summary>
    /// The IQueryDataStore is used by commands
    /// </summary>
    public interface ICommandDataStore
    {
        Task<T> AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : Entity;

        Task RemoveByIdAsync<T>(long id, CancellationToken cancellationToken) where T : Entity;
    }
}