using FluentValidation;
using MediConsult.Application.UsesCases.Patients.Request;

namespace MediConsult.Application.Validators;

public class PatientUpdateValidator : AbstractValidator<UpdatePatientRequest>
{
    public PatientUpdateValidator()
    {
        
        RuleFor(p => p.FirstName)
            .NotNull()
            .WithMessage("The First name is required.");

        RuleFor(p => p.LastName)
            .NotNull()
            .WithMessage("The Last name is required.");

        RuleFor(p => p.Document)
            .NotNull()
            .WithMessage("The Document is required.")
            .MaximumLength(13)
            .WithMessage("The Document is too long.");

        RuleFor(p => p.Gender)
          .NotNull()
          .WithMessage("The Gender is required.")
          .MaximumLength(6)
          .WithMessage("The gender is too long.");

        RuleFor(p => p.Address)
          .NotNull()
          .WithMessage("The Address is required.")
          .MaximumLength(500)
          .WithMessage("The Address is too long.");

        RuleFor(p => p.Phone)
           .NotNull()
           .WithMessage("The Phone is required.")
           .MaximumLength(11)
           .WithMessage("The Phone is too long.");
    }
}
