using JosiArchitecture.Core.Shared.Persistence;
using JosiArchitecture.Core.Todos;
using Microsoft.EntityFrameworkCore;

namespace JosiArchitecture.Data
{
    public class DataStore : DbContext, IApplicationDbContext
    {
        public DataStore(DbContextOptions<DataStore> options) : base(options)
        {
        }

        public DbSet<TodoList> TodoLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UseSingularTableNames(modelBuilder);
        }

        private static void UseSingularTableNames(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
            }
        }
    }
}