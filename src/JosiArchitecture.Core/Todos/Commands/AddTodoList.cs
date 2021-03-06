﻿using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Core.Shared;
using JosiArchitecture.Core.Shared.Cqs;

namespace JosiArchitecture.Core.Todos.Commands
{
    public class AddTodoListHandler : ICommandHandler<AddTodoListCommand>
    {
        private readonly ICommandDataStore _store;

        public AddTodoListHandler(ICommandDataStore store)
        {
            _store = store;
        }

        public async Task Handle(AddTodoListCommand request, CancellationToken cancellationToken)
        {
            var todoList = new TodoList(request.Title);
            await _store.AddAsync(todoList, cancellationToken);
        }
    }

    public class AddTodoListCommand : ICommand
    {
        public string Title { get; set; }
    }
}