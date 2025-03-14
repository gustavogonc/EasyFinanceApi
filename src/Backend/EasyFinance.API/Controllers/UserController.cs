using EasyFinance.Application.UseCases.User.Register;
using EasyFinance.Communication.Request;
using EasyFinance.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace EasyFinance.API.Controllers;
public class UserController : EasyFinanceBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListUsers([FromServices] IRegisterUserUseCase useCase, [FromBody] RequestRegisterUserJson request)
    {
        var result = await useCase.Execute(request);
        return Created(string.Empty, result);
    }

}

