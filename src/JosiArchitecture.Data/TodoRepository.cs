using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Core.Todos;

namespace JosiArchitecture.Data
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DataStore store;

        public TodoRepository(DataStore store)
        {
            this.store = store;
        }

        public async Task AddAsync(Todo todo, CancellationToken cancellationToken)
        {
            await store.Todos.AddAsync(todo, cancellationToken);
        }

        public async Task RemoveByIdAsync(long id, CancellationToken cancellationToken)
        {
            var todo = await store.Todos.FindAsync(id);

            if (todo == null)
            {
                return;
            }

            store.Todos.Remove(todo);
        }
    }
}
