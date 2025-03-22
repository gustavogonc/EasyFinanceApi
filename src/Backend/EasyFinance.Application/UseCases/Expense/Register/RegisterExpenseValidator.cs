using EasyFinance.Communication.Request;
using EasyFinance.Exceptions;
using FluentValidation;

namespace EasyFinance.Application.UseCases.Expense.Register;
public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpenseJson>
{
    public RegisterExpenseValidator()
    {
        RuleFor(expense => expense.Title).NotEmpty().WithMessage(ResourceMessageException.EXPENSE_EMPTY_TITLE);
        RuleFor(expense => expense.Value).GreaterThan(0).WithMessage(ResourceMessageException.EXPENSE_GREATHER_THAN_ZERO);
        RuleFor(expense => expense.PaymentMethod).NotNull().WithMessage(ResourceMessageException.EXPENSE_PAYMENT_METHOD_EMPTY);
        RuleFor(expense => expense.Type).NotNull().WithMessage(ResourceMessageException.EXPENSE_TYPE_EMPTY);
        RuleFor(expense => expense.Category).NotNull().WithMessage(ResourceMessageException.EXPENSE_CATEGORY_EMPTY);
        When(expense => expense.Months != null, () =>
        {
            RuleFor(expense => expense.Months).GreaterThan(0).WithMessage(ResourceMessageException.EXPENSE_MONTHS_GREATHER_THAN_ZERO);
        });
    }
}

