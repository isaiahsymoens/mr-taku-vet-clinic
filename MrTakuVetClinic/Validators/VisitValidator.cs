using FluentValidation;
using MrTakuVetClinic.Entities;

namespace MrTakuVetClinic.Validators
{
    public class VisitValidator : AbstractValidator<Visit>
    {
        public VisitValidator()
        {
            RuleFor(v => v.VisitTypeId)
                .NotEmpty().WithMessage("Visit type ID is required.");
            //RuleFor(v => v.Date)
            //        .NotEmpty().WithMessage("Date is required.");
            RuleFor(v => v.PetId)
               .NotEmpty().WithMessage("Pet ID is required.");
            RuleFor(v => v.Notes)
                    .MaximumLength(100).WithMessage("Notes cannot exceed 100 characters.");
        }
    }
}
