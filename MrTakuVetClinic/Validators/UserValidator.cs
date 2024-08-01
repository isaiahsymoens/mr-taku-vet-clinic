using FluentValidation;
using MrTakuVetClinic.Entities;

namespace MrTakuVetClinic.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");
            RuleFor(u => u.MiddleName)
               .MaximumLength(50).WithMessage("Middle name cannot exceed 50 characters.");
            RuleFor(u => u.LastName)
               .NotEmpty().WithMessage("Last name is required.")
               .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");
            RuleFor(u => u.Email)
                .EmailAddress().WithMessage("A valid email is required.");
            RuleFor(u => u.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(30).WithMessage("Username cannot exceed 30 characters.");
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required.");
            RuleFor(u => u.UserTypeId)
                .NotEmpty().WithMessage("User type id is required.");
        }
    }
}
