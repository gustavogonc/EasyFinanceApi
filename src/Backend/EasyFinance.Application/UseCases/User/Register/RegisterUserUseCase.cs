using EasyFinance.Communication.Request;
using EasyFinance.Domain.Repositories;
using EasyFinance.Domain.Repositories.User;
using EasyFinance.Domain.Security.Cryptography;
using EasyFinance.Exceptions.ExceptionBase;

namespace EasyFinance.Application.UseCases.User.Register;
public class RegisterUserUseCase(IUserWriteOnlyRepository repository, IUserReadOnlyRepository readOnlyRepository, 
                                 IUnitOfWork unitOfWork, IPasswordEncrypter passwordEncrypter) : IRegisterUserUseCase
{
    public async Task Execute(RequestRegisterUserJson request)
    {
        await ValidateAsync(request);

        Domain.Entities.User user = new()
        {
            Email = request.Email,
            Name = request.Name,
            Password = passwordEncrypter.Encrypt(request.Password),
        };

        await repository.RegisterUserAsync(user);

        await unitOfWork.CommitAsync();
    }

    private async Task ValidateAsync(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = await validator.ValidateAsync(request);

        var existsUser = await readOnlyRepository.ExistsUserWithEmailAsync(request.Email);

        if (existsUser) 
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "Email already registered."));
        }

        if (!result.IsValid)
        {
            IList<string> errors = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errors);
        }
    }
}

