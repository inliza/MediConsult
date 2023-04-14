using FluentValidation;
using MediConsult.Application.UsesCases.Patients.Request;
using MediConsult.Application.UsesCases.Users.Request;

namespace MediConsult.Application.Validators;

public class UserCreateValidator : AbstractValidator<CreateUserRequest>
{
    public UserCreateValidator()
    {
        RuleFor(p => p.UserName)
           .NotNull()
           .WithMessage("The fusername is required.")
           .MaximumLength(50)
            .WithMessage("The username is too long.");
        RuleFor(p => p.FirstName)
            .NotNull()
            .WithMessage("The first name is required.")
            .MaximumLength(50)
            .WithMessage("The first name is too long.");
        RuleFor(p => p.LastName)
            .NotNull()
            .WithMessage("The last name is required.")
            .MaximumLength(50)
            .WithMessage("The Last Name is too long.");
        RuleFor(p => p.Password)
            .NotNull()
            .WithMessage("The document is required.");
    }
}
