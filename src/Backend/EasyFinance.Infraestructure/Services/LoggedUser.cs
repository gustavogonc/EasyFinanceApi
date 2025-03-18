using EasyFinance.Domain.Entities;
using EasyFinance.Domain.Security.Tokens;
using EasyFinance.Domain.Services.LoggedUser;
using EasyFinance.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EasyFinance.Infraestructure.Services;
public class LoggedUser(ITokenProvider tokenProvider, EasyFinanceDbContext dbContext) : ILoggedUser
{
    public async Task<User> User()
    {
        var token = tokenProvider.Value();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        var userIdentifier = Guid.Parse(identifier);

        return await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Active && user.UserIdentifier == userIdentifier);
    }
}

