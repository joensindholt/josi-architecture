using JosiArchitecture.Core.Shared.Persistence;
using JosiArchitecture.Core.Users;
using JosiArchitecture.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace JosiArchitecture.Data
{
    public class DataStore : DbContext, IApplicationDbContext
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public DataStore(DbContextOptions<DataStore> options, IOptions<DatabaseOptions> databaseOptions) : base(options)
        {
            _databaseOptions = databaseOptions;
        }

        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (_databaseOptions.Value.UseSingularTableNames == true)
            {
                UseSingularTableNames(modelBuilder);
            }

            if (_databaseOptions.Value.Schema is not null)
            {
                modelBuilder.HasDefaultSchema(_databaseOptions.Value.Schema);
            }

            // // Users seems to be a reserved word in Postgres
            // modelBuilder.Entity<User>().ToTable("FooUsersTbl");
            // modelBuilder.Entity<Profile>().ToTable("FooProfileTbl");
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