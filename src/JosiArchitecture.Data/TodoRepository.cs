using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Core.Todos;

namespace JosiArchitecture.Data
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DataStore dbContext;

        public TodoRepository(DataStore dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(Todo todo, CancellationToken cancellationToken)
        {
            await dbContext.Todos.AddAsync(todo, cancellationToken);
        }

        public async Task RemoveByIdAsync(long id, CancellationToken cancellationToken)
        {
            var todo = await dbContext.Todos.FindAsync(id);

            if (todo == null)
            {
                return;
            }

            dbContext.Todos.Remove(todo);
        }
    }
}