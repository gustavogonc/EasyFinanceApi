using Bogus;
using EasyFinance.Communication.Enum;
using EasyFinance.Communication.Request;

namespace CommonTestUtilities.Requests;
public class RequestRegisterExpenseJsonBuilder
{
    public static RequestRegisterExpenseJson Build()
    {
        return new Faker<RequestRegisterExpenseJson>()
            .RuleFor(expense => expense.Title, (f) => f.Commerce.ProductName())
            .RuleFor(expense => expense.Value, (f) => f.Random.Decimal(1, 500000000))
            .RuleFor(expense => expense.Category, (f) => f.PickRandom<Category>())
            .RuleFor(expense => expense.PaymentMethod, (f) => f.PickRandom<PaymentMethod>())
            .RuleFor(expense => expense.IsRecurrent, (f) => f.Random.Bool())
            .RuleFor(expense => expense.IsRecurrentActive, (f) => f.Random.Bool())
            .RuleFor(expense => expense.Date, (f) => f.Date.BetweenDateOnly(DateOnly.Parse("2025-01-01"), DateOnly.Parse("2025-03-31")))
            .RuleFor(expense => expense.Type, (f) => f.PickRandom<ExpenseType>())
            .RuleFor(expense => expense.Months, (f) => f.Finance.Random.Int(1, 10));
    }
}

