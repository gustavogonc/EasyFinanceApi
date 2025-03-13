using EasyFinance.Application.SharedValidators;
using EasyFinance.Communication.Request;
using EasyFinance.Exceptions;
using FluentValidation;

namespace EasyFinance.Application.UseCases.User.Register;
public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor((user) => user.Name).NotEmpty().WithMessage(ResourceMessageException.NAME_EMPTY);
        RuleFor((user) => user.Email).NotEmpty().WithMessage(ResourceMessageException.EMAIL_EMPTY);
        RuleFor((user) => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
        When((user) => user.Email != null && user.Email != string.Empty, () =>
        {
            RuleFor((user) => user.Email).EmailAddress().WithMessage(ResourceMessageException.EMAIL_INVALID);
        });
    }
}

