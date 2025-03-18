using EasyFinance.API.Attributes;
using EasyFinance.Application.UseCases.Expense.Register;
using EasyFinance.Communication.Request;
using Microsoft.AspNetCore.Mvc;

namespace EasyFinance.API.Controllers;
public class ExpenseController : EasyFinanceBaseController
{

    [HttpPost]
    [AuthenticatedUser]
    public async Task<IActionResult> RegisterExpense([FromServices] IRegisterExpenseUseCase useCase, [FromBody] RequestRegisterExpense request)
    {
        await useCase.Execute(request);
        return Created(string.Empty, string.Empty);
    }
}

