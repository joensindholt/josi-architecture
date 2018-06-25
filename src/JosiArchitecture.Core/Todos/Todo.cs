using System.ComponentModel.DataAnnotations;

namespace JosiArchitecture.Core.Todos
{
    public class Todo
    {
        public Todo(string title)
        {
            Title = title;
        }

        private Todo()
        {
            // For EF and such
        }

        public long Id { get; private set; }

        [Required]
        [StringLength(500)]
        public string Title { get; private set; }
    }
}