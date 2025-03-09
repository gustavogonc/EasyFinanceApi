using EasyFinance.Communication.Request;
using EasyFinance.Communication.Response;

namespace EasyFinance.Application.UseCases.Login;
public interface IDoLoginUseCase
{
    Task<ResponseTokensJson> Execute(RequestLoginJson request);
}

