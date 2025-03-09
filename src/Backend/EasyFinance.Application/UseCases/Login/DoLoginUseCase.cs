using EasyFinance.Communication.Request;
using EasyFinance.Communication.Response;
using EasyFinance.Domain.Repositories.User;
using EasyFinance.Domain.Security.Cryptography;
using EasyFinance.Domain.Security.Tokens;
using EasyFinance.Exceptions.ExceptionBase;

namespace EasyFinance.Application.UseCases.Login;
public class DoLoginUseCase(IUserReadOnlyRepository readOnlyRepository, IAccessTokenGenerator tokenGenerator,
                            IPasswordEncrypter passwordEncrypter) : IDoLoginUseCase
{
    public async Task<ResponseTokensJson> Execute(RequestLoginJson request)
    {
        var user = await readOnlyRepository.GetByEmailAsync(request.Email);

        if (user is null || !passwordEncrypter.IsValid(request.Password, user.Password))
        {
            throw new InvalidLoginException();
        }

        return new ResponseTokensJson
        {
            AccessToken = tokenGenerator.Generate(user.UserIdentifier),
            RefreshToken = string.Empty
        };
    }
}

