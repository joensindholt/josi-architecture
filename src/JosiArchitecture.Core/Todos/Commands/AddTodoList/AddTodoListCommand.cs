using JosiArchitecture.Core.Shared.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Todos.Commands.AddTodoList
{
    public class AddTodoListCommand : IRequest<int>
    {
        public string Title { get; set; }
    }

    public class AddTodoListHandler : IRequestHandler<AddTodoListCommand, int>
    {
        private readonly IApplicationDbContext _db;

        public AddTodoListHandler(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<int> Handle(AddTodoListCommand request, CancellationToken cancellationToken)
        {
            var list = new TodoList(request.Title);
            await _db.TodoLists.AddAsync(list);
            await _db.SaveChangesAsync(cancellationToken);
            return list.Id;
        }
    }
}