using FluentValidation;
using MediConsult.Application.UsesCases.Auth.Request;


namespace MediConsult.Application.Validators
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(p => p.Username)
                .NotNull()
                .WithMessage("The Username is required.");

            RuleFor(p => p.Password)
                .NotNull()
                .WithMessage("The Password is required.");
        }
    }
}
