using EasyFinance.Communication.Request;
using FluentValidation;

namespace EasyFinance.Application.UseCases.User.Register;
public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor((user) => user.Name).NotEmpty().WithMessage("The name must be filled");
        RuleFor((user) => user.Email).NotEmpty().WithMessage("The email field must be filled");
        When((user) => user.Email != null, () =>
        {
            RuleFor((user) => user.Email).EmailAddress().WithMessage("The email must be a valid email address");
        });
    }
}

