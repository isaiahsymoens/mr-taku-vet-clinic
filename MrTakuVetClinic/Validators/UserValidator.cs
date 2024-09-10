using FluentValidation;
using MrTakuVetClinic.DTOs.User;

namespace MrTakuVetClinic.Validators
{
    public class UserValidator : AbstractValidator<UserPostDto>
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
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");
            RuleFor(u => u.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(6).WithMessage("Username must be at least 6 characters long")
                .MaximumLength(30).WithMessage("Username cannot exceed 30 characters.");
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one number")
                .Matches(@"[\W]").WithMessage("Password must contain at least one special character")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.");
            RuleFor(u => u.UserTypeId)
                .NotEmpty().WithMessage("User type id is required.");
        }
    }
}
