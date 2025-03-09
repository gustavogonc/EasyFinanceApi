using System.Net;

namespace EasyFinance.Exceptions.ExceptionBase;
public class InvalidLoginException() : EasyFinanceException(ResourceMessageException.EMAIL_OR_PASSWORD_INVALID)
{
    public override IList<string> GetErrorMessages() => [Message];
    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}


