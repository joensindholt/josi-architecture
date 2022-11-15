using FluentValidation;

namespace JosiArchitecture.Core.Todos.Commands.AddTodoList
{
    public class AddTodoListValidator : AbstractValidator<AddTodoListCommand>
    {
        public AddTodoListValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty();
        }
    }
}