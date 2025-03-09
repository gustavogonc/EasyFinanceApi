using EasyFinance.Exceptions;
using FluentValidation;
using FluentValidation.Validators;

namespace EasyFinance.Application.SharedValidators;
public class PasswordValidator<T> : PropertyValidator<T, string>
{
    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMessageException.PASSWORD_EMPTY);
            return false;
        }

        if (password.Length < 8)
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMessageException.MINIMUM_PASSWORD_LENGTH);
            return false;
        }

        return true;
    }

    public override string Name => throw new NotImplementedException();
}

