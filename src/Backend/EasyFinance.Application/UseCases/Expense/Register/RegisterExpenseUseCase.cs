using EasyFinance.Communication.Request;
using EasyFinance.Domain.Repositories;
using EasyFinance.Domain.Repositories.Expense;
using EasyFinance.Domain.Services.LoggedUser;

namespace EasyFinance.Application.UseCases.Expense.Register;
public class RegisterExpenseUseCase(IExpenseWriteOnlyRepository writeOnlyRepository, IUnitOfWork unitOfWork, 
                                    ILoggedUser loggedUser) : IRegisterExpenseUseCase
{
    public async Task Execute(RequestRegisterExpense request)
    {
        var user = await loggedUser.User();
        var expense = new Domain.Entities.Expense
        {
            Title = request.Title,
            Category = (Domain.Enum.Category)request.Category,
            Months = request.Months,
            PaymentMethod = (Domain.Enum.PaymentMethod)request.PaymentMethod,
            Date = request.Date,
            StartMonth = request.Date.Month,
            StartYear = request.Date.Year,
            Type = (Domain.Enum.ExpenseType)request.Type,
            Value = request.Value,
            UserId = user.Id
        };

        await writeOnlyRepository.AddExpenseAsync(expense);
        await unitOfWork.CommitAsync();
    }
}

