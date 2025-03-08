using EasyFinance.Application.UseCases.User.Register;
using EasyFinance.Communication.Request;
using Microsoft.AspNetCore.Mvc;

namespace EasyFinance.API.Controllers;
public class UserController : EasyFinanceBaseController
{
    [HttpPost]
    public async Task<IActionResult> ListUsers([FromServices] IRegisterUserUseCase useCase, [FromBody]RequestRegisterUserJson request)
    {
        await useCase.Execute(request);
        return Ok();
    }

}

