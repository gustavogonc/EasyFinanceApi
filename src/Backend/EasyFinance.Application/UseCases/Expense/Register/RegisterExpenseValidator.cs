using EasyFinance.Communication.Request;
using EasyFinance.Exceptions;
using FluentValidation;

namespace EasyFinance.Application.UseCases.Expense.Register;
public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpense>
{
    public RegisterExpenseValidator()
    {
        RuleFor(expense => expense.Title).NotEmpty().WithMessage(ResourceMessageException.EXPENSE_EMPTY_TITLE);
        RuleFor(expense => expense.Value).GreaterThan(0).WithMessage(ResourceMessageException.EXPENSE_GREATHER_THAN_ZERO);
        RuleFor(expense => expense.PaymentMethod).NotEmpty().WithMessage(ResourceMessageException.EXPENSE_PAYMENT_METHOD_EMPTY);
        RuleFor(expense => expense.Type).NotEmpty().WithMessage(ResourceMessageException.EXPENSE_TYPE_EMPTY);
        RuleFor(expense => expense.Category).NotEmpty().WithMessage(ResourceMessageException.EXPENSE_CATEGORY_EMPTY);
        When(expense => expense.Months != null, () =>
        {
            RuleFor(expense => expense.Months).GreaterThan(0).WithMessage(ResourceMessageException.EXPENSE_MONTHS_GREATHER_THAN_ZERO);
        });
    }
}

