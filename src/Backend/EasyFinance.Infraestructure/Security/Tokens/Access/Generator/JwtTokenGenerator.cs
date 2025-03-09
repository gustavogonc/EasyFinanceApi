using EasyFinance.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EasyFinance.Infraestructure.Security.Tokens.Access.Generator;
public class JwtTokenGenerator : JwtTokenHandler, IAccessTokenGenerator
{
    private readonly uint _expirationTimesMinutes;
    private readonly string _signinKey;

    public JwtTokenGenerator(uint expirationTimesMinutes, string sigInKey)
    {
        _expirationTimesMinutes = expirationTimesMinutes;
        _signinKey = sigInKey;
    }
    public string Generate(Guid userIdentifier)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Sid, userIdentifier.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimesMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(_signinKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }
}

