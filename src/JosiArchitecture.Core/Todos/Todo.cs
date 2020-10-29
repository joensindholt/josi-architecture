using JosiArchitecture.Core.Shared;

namespace JosiArchitecture.Core.Todos
{
    public class Todo : Entity
    {
        public Todo(string title)
        {
            Title = title;
        }

        // For EF
        private Todo()
        {
        }

        public string Title { get; private set; }

        public TodoList TodoList { get; set; }
    }
}