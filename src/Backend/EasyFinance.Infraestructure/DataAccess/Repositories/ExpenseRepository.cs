using EasyFinance.Domain.Entities;
using EasyFinance.Domain.Repositories.Expense;
using Microsoft.EntityFrameworkCore;

namespace EasyFinance.Infraestructure.DataAccess.Repositories;
public class ExpenseRepository(EasyFinanceDbContext dbContext) : IExpenseWriteOnlyRepository, IExpenseReadOnlyRepository
{
    public async Task AddExpenseAsync(Expense request) => await dbContext.Expenses.AddAsync(request);
    public async Task AddExpenseListAsync(IList<Expense> request) => await dbContext.Expenses.AddRangeAsync(request);
    public async Task<IList<Expense>> ReturnRecurrentExpensesAsync() => await dbContext.Expenses
                                                                                                  .Where(expense => expense.IsRecurrent && 
                                                                                                  expense.IsRecurrentActive)
                                                                                                  .AsNoTracking()
                                                                                                  .ToListAsync();

    public async Task<IList<Expense>> ReturnUserRecurrentExpensesAsync(long userId) => await dbContext.Expenses
                                                                                                  .Where(expense => expense.IsRecurrent &&
                                                                                                  expense.IsRecurrentActive &&
                                                                                                  expense.UserId == userId)
                                                                                                  .OrderByDescending(expense => expense.CreatedOn)
                                                                                                  .AsNoTracking()
                                                                                                  .ToListAsync();
}

