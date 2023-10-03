using FluentValidation;

namespace JosiArchitecture.Core.Users.Commands.CreateUser;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}