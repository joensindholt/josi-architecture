using JosiArchitecture.Core.Shared.Model;

namespace JosiArchitecture.Core.Todos
{
    public class Todo : IEntity
    {
        public Todo(string title)
        {
            Title = title;
        }

        // For EF
        private Todo()
        {
        }

        public long Id { get; set; }

        public string Title { get; private set; }

        public TodoList TodoList { get; set; }
    }
}
