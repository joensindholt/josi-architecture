using FluentValidation;

namespace JosiArchitecture.Core.Todos.Commands.AddTodo
{
    public class AddTodoValidator : AbstractValidator<AddTodoCommand>
    {
        public AddTodoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty();
        }
    }
}