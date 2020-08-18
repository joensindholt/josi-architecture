using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Core.Shared;
using JosiArchitecture.Core.Todos;
using Microsoft.EntityFrameworkCore;

namespace JosiArchitecture.Data
{
    public class DataStore : DbContext, IQueryDataStore, IUnitOfWork
    {
        public DataStore(DbContextOptions<DataStore> options)
           : base(options)
        { }


        public DbSet<Todo> Todos { get; set; }

        IQueryable<Todo> IQueryDataStore.Todos => Todos;

        public async Task CompleteAsync(CancellationToken cancellationToken)
        {
            await SaveChangesAsync(cancellationToken);
        }
    }
}