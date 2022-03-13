﻿using JosiArchitecture.Core.Shared.Persistence;
using JosiArchitecture.Core.Todos;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Data
{
    public class DataStore : DbContext, IQueryDataStore, ICommandDataStore, IUnitOfWork
    {
        public DataStore(DbContextOptions<DataStore> options)
           : base(options)
        { }

        public DbSet<TodoList> TodoLists { get; set; }

        IQueryable<TodoList> IQueryDataStore.TodoLists => TodoLists;

        public async Task CompleteAsync(CancellationToken cancellationToken)
        {
            await SaveChangesAsync(cancellationToken);
        }

        async Task ICommandDataStore.RemoveByIdAsync<T>(long id, CancellationToken cancellationToken)
        {
            var set = Set<T>();

            var entity = await set.FindAsync(id, cancellationToken);

            if (entity == null)
            {
                return;
            }

            set.Remove(entity);
        }

        async Task<T> ICommandDataStore.AddAsync<T>(T entity, CancellationToken cancellationToken)
        {
            var result = await base.AddAsync(entity, cancellationToken);
            return result.Entity;
        }
    }
}