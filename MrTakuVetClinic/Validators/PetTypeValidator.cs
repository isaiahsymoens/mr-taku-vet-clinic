using FluentValidation;
using MrTakuVetClinic.DTOs.PetType;

namespace MrTakuVetClinic.Validators
{
    public class PetTypeValidator : AbstractValidator<PetTypePostDto>
    {
        public PetTypeValidator()
        {
            RuleFor(p => p.TypeName)
               .NotEmpty().WithMessage("Type name is required.")
               .MaximumLength(50).WithMessage("Type name cannot exceed 50 characters.");
        }
    }
}
