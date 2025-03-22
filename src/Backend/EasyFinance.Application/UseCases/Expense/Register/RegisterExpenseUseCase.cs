using AutoMapper;
using EasyFinance.Communication.Request;
using EasyFinance.Domain.Repositories;
using EasyFinance.Domain.Repositories.Expense;
using EasyFinance.Domain.Services.LoggedUser;
using EasyFinance.Exceptions.ExceptionBase;

namespace EasyFinance.Application.UseCases.Expense.Register;
public class RegisterExpenseUseCase(IExpenseWriteOnlyRepository writeOnlyRepository, IUnitOfWork unitOfWork, 
                                    ILoggedUser loggedUser, IMapper mapper) : IRegisterExpenseUseCase
{
    public async Task Execute(RequestRegisterExpenseJson request)
    {
        await Validate(request);

        var user = await loggedUser.User();

        if (request.Months > 0)
        {
            await DivideExpenses(request, user);
        }
        else
        {
            var expense = mapper.Map<Domain.Entities.Expense>(request);

            expense.UserId = user.Id;
            expense.StartMonth = request.Date.Month;
            expense.StartYear = request.Date.Year;

            await writeOnlyRepository.AddExpenseAsync(expense);
        }

        await unitOfWork.CommitAsync();
    }

    private async Task DivideExpenses(RequestRegisterExpenseJson request, Domain.Entities.User user)
    {
        IList<Domain.Entities.Expense> expensesList = [];

        for (int i = 1; i <= request.Months; i++)
        {
            var expenseItem = mapper.Map<Domain.Entities.Expense>(request);
            expenseItem.UserId = user.Id;
            expenseItem.StartMonth = request.Date.Month;
            expenseItem.StartYear = request.Date.Year;

            if (i > 1)
            {
                expenseItem.Date = request.Date.AddMonths(i - 1);
            }

            expenseItem.Value = Math.Ceiling(request.Value / (decimal)request.Months);

            expensesList.Add(expenseItem);
        }

        await writeOnlyRepository.AddExpenseListAsync(expensesList);
    }

    private static async Task Validate(RequestRegisterExpenseJson request)
    {
        var validator = new RegisterExpenseValidator();

        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errors);
        }
    }
}

