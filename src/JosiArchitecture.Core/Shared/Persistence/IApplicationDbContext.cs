using JosiArchitecture.Core.Todos;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Shared.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}