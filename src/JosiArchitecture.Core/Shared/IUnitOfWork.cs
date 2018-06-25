using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Shared
{
    public interface IUnitOfWork
    {
        Task CompleteAsync(CancellationToken cancellationToken);
    }
}