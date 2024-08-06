using FluentValidation;
using MrTakuVetClinic.DTOs.Pet;

namespace MrTakuVetClinic.Validators
{
    public class PetValidator : AbstractValidator<PetPostDto>
    {
        public PetValidator()
        {
            RuleFor(p => p.Username)
                .NotEmpty().WithMessage("Username is required.");
            RuleFor(p => p.PetName)
                .NotEmpty().WithMessage("Pet name is required.")
                .MaximumLength(50).WithMessage("Pet name cannot exceed 50 characters.");
            RuleFor(p => p.PetTypeId)
                .NotEmpty().WithMessage("Pet type ID is required.");
            RuleFor(p => p.Breed)
                .MaximumLength(50).WithMessage("Breed cannot exceed 50 characters.");
        }
    }
}
