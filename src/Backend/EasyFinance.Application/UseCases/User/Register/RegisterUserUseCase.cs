using EasyFinance.Communication.Request;
using EasyFinance.Domain.Repositories;
using EasyFinance.Domain.Repositories.User;
using EasyFinance.Exceptions.ExceptionBase;

namespace EasyFinance.Application.UseCases.User.Register;
public class RegisterUserUseCase(IUserWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IRegisterUserUseCase
{
    public async Task Execute(RequestRegisterUserJson request)
    {
        await ValidateAsync(request);

        Domain.Entities.User user = new()
        {
            Email = request.Email,
            Name = request.Name,
            Password = request.Password,
        };

        await repository.RegisterUserAsync(user);

        await unitOfWork.CommitAsync();
    }

    private async Task ValidateAsync(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            IList<string> errors = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errors);
        }
    }
}

