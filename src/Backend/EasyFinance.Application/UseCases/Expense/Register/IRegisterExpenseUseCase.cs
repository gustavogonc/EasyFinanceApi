using EasyFinance.Communication.Request;

namespace EasyFinance.Application.UseCases.Expense.Register;
public interface IRegisterExpenseUseCase
{
    Task Execute(RequestRegisterExpenseJson request);
}

