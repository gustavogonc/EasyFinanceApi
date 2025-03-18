using EasyFinance.Domain.Security.Tokens;

namespace EasyFinance.API.Token
{
    public class HttpContextTokenValue(IHttpContextAccessor contextAccessor) : ITokenProvider
    {
        public string Value()
        {
            var authentication = contextAccessor.HttpContext.Request.Headers.Authorization.ToString();

            return authentication["Bearer ".Length..].Trim();
        }
    }
}
