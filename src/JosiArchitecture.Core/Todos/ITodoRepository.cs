using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Todos
{
    public interface ITodoRepository
    {
        Task AddAsync(Todo todo, CancellationToken cancellationToken);

        Task RemoveByIdAsync(long id, CancellationToken cancellationToken);
    }
}