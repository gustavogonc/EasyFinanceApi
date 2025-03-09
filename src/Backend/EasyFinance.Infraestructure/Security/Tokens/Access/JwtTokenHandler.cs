using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EasyFinance.Infraestructure.Security.Tokens.Access;
public abstract class JwtTokenHandler
{
    protected static SymmetricSecurityKey SecurityKey(string signInKey)
    {
        var bytes = Encoding.UTF8.GetBytes(signInKey);
        return new SymmetricSecurityKey(bytes);
    }
}

