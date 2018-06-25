using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Core.Shared.Cqs;

namespace JosiArchitecture.Core.Contacts.Commands
{
    public class AddContactHandler : ICommandHandler<AddContactCommand>
    {
        private readonly IContactRepository _contactRepository;

        public AddContactHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task Handle(AddContactCommand request, CancellationToken cancellationToken)
        {
            var contact = new Contact(request.Title);
            await _contactRepository.AddAsync(contact, cancellationToken);
        }
    }

    public class AddContactCommand : ICommand
    {
        public string Title { get; }
    }
}