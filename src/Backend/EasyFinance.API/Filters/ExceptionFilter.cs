using EasyFinance.Communication.Response;
using EasyFinance.Exceptions;
using EasyFinance.Exceptions.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasyFinance.API.Filters;
public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is EasyFinanceException easyFinanceException)
            HandleProjectException(easyFinanceException, context);
        else
            ThrowUnknowException(context);
    }

    private void HandleProjectException(EasyFinanceException exception, ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)exception.GetStatusCode();
        context.Result = new ObjectResult(new ResponseErrorJson(exception.GetErrorMessages()));
    }

    private void ThrowUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessageException.UNKNOWN_ERROR));
    }
}

