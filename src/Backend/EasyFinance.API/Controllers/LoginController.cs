using EasyFinance.Application.UseCases.Login;
using EasyFinance.Communication.Request;
using Microsoft.AspNetCore.Mvc;

namespace EasyFinance.API.Controllers;
public class LoginController : EasyFinanceBaseController
{
    [HttpPost]
    public async Task<IActionResult> DoLogin([FromServices] IDoLoginUseCase useCase, [FromBody]RequestLoginJson request)
    {
        var result = await useCase.Execute(request);
        return Ok(result);
    }
}

