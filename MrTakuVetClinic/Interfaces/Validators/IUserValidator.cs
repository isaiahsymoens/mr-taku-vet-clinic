using FluentValidation;
using MrTakuVetClinic.Entities;

namespace MrTakuVetClinic.Interfaces.Validators
{
    public interface IUserValidator : IValidator<User>
    {
    }
}
