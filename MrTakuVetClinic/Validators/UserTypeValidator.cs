using FluentValidation;
using MrTakuVetClinic.DTOs.UserType;

namespace MrTakuVetClinic.Validators
{
    public class UserTypeValidator : AbstractValidator<UserTypePostDto>
    {
        public UserTypeValidator()
        {
            RuleFor(u => u.TypeName)
                  .NotEmpty().WithMessage("Type name is required.")
                  .MaximumLength(50).WithMessage("Type name cannot exceed 50 characters.");
        }
    }
}
