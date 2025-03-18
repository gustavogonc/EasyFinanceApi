using EasyFinance.Domain.Entities;
using EasyFinance.Domain.Repositories.Expense;

namespace EasyFinance.Infraestructure.DataAccess.Repositories;
public class ExpenseRepository(EasyFinanceDbContext dbContext) : IExpenseWriteOnlyRepository
{
    public async Task AddExpenseAsync(Expense request) => await dbContext.Expenses.AddAsync(request);
    public async Task AddExpenseListAsync(IList<Expense> request) => await dbContext.Expenses.AddRangeAsync(request);
}

