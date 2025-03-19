namespace EasyFinance.Domain.Repositories.Expense;
public interface IExpenseReadOnlyRepository
{
    Task<IList<Entities.Expense>> ReturnUserRecurrentExpensesAsync(long userId);
    Task<IList<Entities.Expense>> ReturnRecurrentExpensesAsync();
}

