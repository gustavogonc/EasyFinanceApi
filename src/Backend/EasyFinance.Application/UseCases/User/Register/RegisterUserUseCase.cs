using AutoMapper;
using EasyFinance.Communication.Request;
using EasyFinance.Communication.Response;
using EasyFinance.Domain.Repositories;
using EasyFinance.Domain.Repositories.User;
using EasyFinance.Domain.Security.Cryptography;
using EasyFinance.Domain.Security.Tokens;
using EasyFinance.Exceptions;
using EasyFinance.Exceptions.ExceptionBase;

namespace EasyFinance.Application.UseCases.User.Register;
public class RegisterUserUseCase(IUserWriteOnlyRepository repository, IUserReadOnlyRepository readOnlyRepository, 
                                 IUnitOfWork unitOfWork, IPasswordEncrypter passwordEncrypter,
                                 IMapper mapper, IAccessTokenGenerator tokenGenerator) : IRegisterUserUseCase
{
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await ValidateAsync(request);

        var user = mapper.Map<Domain.Entities.User>(request);

        user.Password = passwordEncrypter.Encrypt(request.Password);   

        await repository.RegisterUserAsync(user);

        await unitOfWork.CommitAsync();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens =
            {
                AccessToken = tokenGenerator.Generate(user.UserIdentifier),
                RefreshToken = string.Empty
            }
        };
    }

    private async Task ValidateAsync(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = await validator.ValidateAsync(request);

        var existsUser = await readOnlyRepository.ExistsUserWithEmailAsync(request.Email);

        if (existsUser) 
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessageException.EMAIL_ALREADY_REGISTERED));
        }

        if (!result.IsValid)
        {
            IList<string> errors = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errors);
        }
    }
}

