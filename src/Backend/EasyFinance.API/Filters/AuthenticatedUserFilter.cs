using EasyFinance.Communication.Response;
using EasyFinance.Domain.Repositories.User;
using EasyFinance.Domain.Security.Tokens;
using EasyFinance.Exceptions;
using EasyFinance.Exceptions.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace EasyFinance.API.Filters;
public class AuthenticatedUserFilter(IUserReadOnlyRepository repository, IAccessTokenValidator accessTokenValidator) : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
		try
		{
			var token = TokenOnRequest(context);

			var userIdentifier = accessTokenValidator.ValidateAndGetUserIdentifier(token);

			var exist = await repository.ExistActiveUserWithIdentifierAsync(userIdentifier);

			if (!exist)
				throw new UnauthorizedException(ResourceMessageException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);

        }
		catch (SecurityTokenExpiredException)
		{
			context.Result = new UnauthorizedObjectResult(new ResponseErrorJson("TokenIsExpired")
			{
				TokenIsExpired = true,
			});
		}
		catch (EasyFinanceException ex)
		{
			context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Message));
		}
		catch
		{
			context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceMessageException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
		}
    }

    private static string TokenOnRequest(AuthorizationFilterContext context)
    {
		var authentication = context.HttpContext.Request.Headers.Authorization.ToString();

		if (string.IsNullOrWhiteSpace(authentication))
			throw new UnauthorizedException(ResourceMessageException.NO_TOKEN);

		return authentication["Bearer ".Length..].Trim();
    }
}

