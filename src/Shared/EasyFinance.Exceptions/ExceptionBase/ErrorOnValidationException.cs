using System.Net;

namespace EasyFinance.Exceptions.ExceptionBase;
public class ErrorOnValidationException(IList<string> errors) : EasyFinanceException(string.Empty)
{
    public override IList<string> GetErrorMessages() => errors;

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}

