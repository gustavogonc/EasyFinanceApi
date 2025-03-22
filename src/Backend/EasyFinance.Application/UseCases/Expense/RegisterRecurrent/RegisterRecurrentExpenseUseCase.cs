using AutoMapper;
using EasyFinance.Domain.Repositories;
using EasyFinance.Domain.Repositories.Expense;
using EasyFinance.Domain.Repositories.User;

namespace EasyFinance.Application.UseCases.Expense.RegisterRecurrent;

public class RegisterRecurrentExpenseUseCase(IExpenseReadOnlyRepository readOnlyRepository, IUserReadOnlyRepository userReadOnlyRepository,
                                       IExpenseWriteOnlyRepository writeOnlyRepository, IUnitOfWork unitOfWork) : IRegisterRecurrentExpenseUseCase
{


    public async Task Execute()
    {
        var users = await userReadOnlyRepository.ListActiveUsersAsync().ConfigureAwait(false);

        foreach (var user in users)
        {
            var recurrentExpenses = await GetDistinctRecurrentExpensesAsync(user.Id).ConfigureAwait(false);
            var expensesToAdd = GetExpensesToAdd(recurrentExpenses, user.Id);

            if (expensesToAdd.Count > 0)
            {
                await writeOnlyRepository.AddExpenseListAsync(expensesToAdd).ConfigureAwait(false);
                await unitOfWork.CommitAsync().ConfigureAwait(false);
            }
        }
    }

    private async Task<List<Domain.Entities.Expense>> GetDistinctRecurrentExpensesAsync(long userId)
    {
        var recurrentExpenses = await readOnlyRepository.ReturnUserRecurrentExpensesAsync(userId).ConfigureAwait(false);
        return recurrentExpenses.DistinctBy(expense => expense.Title).ToList();
    }

    private static List<Domain.Entities.Expense> GetExpensesToAdd(IEnumerable<Domain.Entities.Expense> recurrentExpenses, long userId)
    {
        var expensesToAdd = new List<Domain.Entities.Expense>();
        var today = DateOnly.FromDateTime(DateTime.Today);

        foreach (var expense in recurrentExpenses)
        {
            if (IsOneMonthOld(expense.Date, today))
            {
                expensesToAdd.Add(new Domain.Entities.Expense
                {
                    Category = expense.Category,
                    IsRecurrent = expense.IsRecurrent,
                    IsRecurrentActive = expense.IsRecurrentActive,
                    Months = expense.Months,
                    Active = expense.Active,
                    PaymentMethod = expense.PaymentMethod,
                    CreatedOn = DateTime.UtcNow,
                    StartMonth = expense.StartMonth,
                    StartYear = expense.StartYear,
                    Title = expense.Title,
                    Type = expense.Type,
                    UserId = userId,
                    Value = expense.Value,
                    Date = today,
                });
            }
        }

        return expensesToAdd;
    }

    private static bool IsOneMonthOld(DateOnly expenseDate, DateOnly today)
    {
        return today.Year == expenseDate.Year && today.Month - expenseDate.Month == 1 ||
               today.Year - expenseDate.Year == 1 && today.Month == 1 && expenseDate.Month == 12;
    }
}