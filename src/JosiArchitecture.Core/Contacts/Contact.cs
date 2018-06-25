using System.ComponentModel.DataAnnotations;

namespace JosiArchitecture.Core.Contacts
{
    public class Contact
    {
        public Contact(string title)
        {
            Title = title;
        }

        private Contact()
        {
            // For EF and such
        }

        public long Id { get; private set; }

        [Required]
        [StringLength(500)]
        public string Title { get; private set; }
    }
}