using System.Net;

namespace EasyFinance.Exceptions.ExceptionBase;
public abstract class EasyFinanceException : SystemException
{
    protected EasyFinanceException(string message) : base(message) { }

    public abstract IList<string> GetErrorMessages();
    public abstract HttpStatusCode GetStatusCode();
}

