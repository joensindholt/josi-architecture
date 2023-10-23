using System.Linq;
using JosiArchitecture.Core.Shared.Persistence;
using JosiArchitecture.Core.Users;
using JosiArchitecture.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
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
            base.OnModelCreating(modelBuilder);

            if (_databaseOptions.Value.UseSingularTableNames == true)
            {
                UseSingularTableNames(modelBuilder);
            }

            if (_databaseOptions.Value.Schema is not null)
            {
                modelBuilder.HasDefaultSchema(_databaseOptions.Value.Schema);
            }

            var converter = new ValueConverter<UserId?, int?>(
                id => id != null ? id.Value : null,
                value => value != null ? new UserId(value.Value) : null,
                new ConverterMappingHints(valueGeneratorFactory: (p, t) => new MyTemporaryIntValueGenerator()));

            modelBuilder.Entity<User>(builder =>
            {
                builder.HasKey(p => p.Id);
                builder.Property(u => u.Id)
                    .HasConversion(converter)
                    .ValueGeneratedOnAdd();
            });

            // // Users seems to be a reserved word in Postgres
            // modelBuilder.Entity<User>().ToTable("FooUsersTbl");
            // modelBuilder.Entity<Profile>().ToTable("FooProfileTbl");
        }

        private static void UseSingularTableNames(ModelBuilder modelBuilder)
        {
            foreach (var clrType in modelBuilder.Model.GetEntityTypes().Select((e => e.ClrType)))
            {
                modelBuilder.Entity(clrType).ToTable(clrType.Name);
            }
        }
    }

    public class MyTemporaryIntValueGenerator : TemporaryIntValueGenerator
    {
        protected override object? NextValue(EntityEntry entry)
        {
            throw new System.NotImplementedException();
        }

        public override bool GeneratesTemporaryValues { get; }
    }
}
