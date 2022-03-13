using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Shared.Persistence
{
    public interface IUnitOfWork
    {
        Task CompleteAsync(CancellationToken cancellationToken);
    }
}