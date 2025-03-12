using EasyFinance.Domain.Security.Tokens;
using EasyFinance.Infraestructure.Security.Tokens.Access.Generator;

namespace CommonTestUtilities.Tokens;
public class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build() => new JwtTokenGenerator(expirationTimesMinutes: 1000, sigInKey: "wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww");
}

