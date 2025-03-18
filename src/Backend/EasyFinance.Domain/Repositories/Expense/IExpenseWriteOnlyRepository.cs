namespace EasyFinance.Domain.Repositories.Expense;
public interface IExpenseWriteOnlyRepository
{
    Task AddExpenseAsync(Entities.Expense request);
    Task AddExpenseListAsync(IList<Entities.Expense> request);
}

