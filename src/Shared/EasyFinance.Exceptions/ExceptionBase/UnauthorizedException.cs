using System.Net;

namespace EasyFinance.Exceptions.ExceptionBase;
public class UnauthorizedException(string message) : EasyFinanceException(message)
{
    public override IList<string> GetErrorMessages() => [Message];
    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}

