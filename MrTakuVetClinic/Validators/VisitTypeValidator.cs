using FluentValidation;
using MrTakuVetClinic.DTOs.VisitType;

namespace MrTakuVetClinic.Validators
{
    public class VisitTypeValidator : AbstractValidator<VisitTypePostDto>
    {
        public VisitTypeValidator()
        {
            RuleFor(v => v.TypeName)
               .NotEmpty().WithMessage("Type name is required.")
               .MaximumLength(50).WithMessage("Type name cannot exceed 50 characters.");
        }
    }
}
