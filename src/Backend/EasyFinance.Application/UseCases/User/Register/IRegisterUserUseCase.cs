using EasyFinance.Communication.Request;

namespace EasyFinance.Application.UseCases.User.Register;
public interface IRegisterUserUseCase
{
    Task Execute(RequestRegisterUserJson request);
}

