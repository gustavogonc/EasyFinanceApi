using EasyFinance.Communication.Request;
using EasyFinance.Communication.Response;

namespace EasyFinance.Application.UseCases.User.Register;
public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}

