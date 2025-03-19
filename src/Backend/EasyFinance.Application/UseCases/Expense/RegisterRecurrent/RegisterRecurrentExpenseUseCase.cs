using EasyFinance.Domain.Repositories;
using EasyFinance.Domain.Repositories.Expense;
using EasyFinance.Domain.Repositories.User;

namespace EasyFinance.Application.UseCases.Expense.RegisterRecurrent;
public class RegisterRecurrentExpenseUseCase(IExpenseReadOnlyRepository readOnlyRepository, IUserReadOnlyRepository userReadOnlyRepository,
                                             IExpenseWriteOnlyRepository writeOnlyRepository, IUnitOfWork unitOfWork) : IRegisterRecurrentExpenseUseCase
{
    public async Task Execute()
    {
        var users = await userReadOnlyRepository.ListActiveUsersAsync();

        foreach(var user in users) 
        {
            var recurrentExpenses = await readOnlyRepository.ReturnUserRecurrentExpensesAsync(user.Id);
            recurrentExpenses = recurrentExpenses.DistinctBy(expense => expense.Title).ToList();

            var expensesToAdd = new List<Domain.Entities.Expense>();
            foreach (var expenses in recurrentExpenses)
            {
                var today = DateOnly.FromDateTime(DateTime.Now.Date);

                if (today.Month - expenses.Date.Month == 1)
                {
                    expensesToAdd.Add(new Domain.Entities.Expense
                    {
                        Category = expenses.Category,
                        IsRecurrent = expenses.IsRecurrent,
                        IsRecurrentActive = expenses.IsRecurrentActive,
                        Months = expenses.Months,
                        Active = expenses.Active,
                        PaymentMethod = expenses.PaymentMethod,
                        CreatedOn = DateTime.UtcNow,
                        StartMonth = expenses.StartMonth,
                        StartYear = expenses.StartYear,
                        Title = expenses.Title,
                        Type = expenses.Type,
                        UserId = user.Id,
                        Value = expenses.Value,
                        Date = today,
                    });
                }
            }

            if (expensesToAdd.Count != 0)
            {
                await writeOnlyRepository.AddExpenseListAsync(expensesToAdd);
                await unitOfWork.CommitAsync();
            }
        }
    }
}

