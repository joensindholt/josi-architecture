using System.Collections.Generic;
using JosiArchitecture.Core.Shared;

namespace JosiArchitecture.Core.Todos
{
    public class TodoList : Entity
    {
        public TodoList(string title) : this()
        {
            Title = title;
        }

        // For EF
        private TodoList()
        {
            Todos = new List<Todo>();
        }

        public string Title { get; private set; }

        public List<Todo> Todos { get; set; }
    }
}