using EasyFinance.API.Attributes;
using EasyFinance.Application.UseCases.Expense.Register;
using EasyFinance.Communication.Request;
using EasyFinance.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace EasyFinance.API.Controllers;
public class ExpenseController : EasyFinanceBaseController
{
    [HttpPost]
    [AuthenticatedUser]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterExpense([FromServices] IRegisterExpenseUseCase useCase, [FromBody] RequestRegisterExpenseJson request)
    {
        await useCase.Execute(request);
        return Created(string.Empty, string.Empty);
    }
}

