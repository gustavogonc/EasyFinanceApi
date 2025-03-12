using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using EasyFinance.Application.UseCases.User.Register;
using Shouldly;
using Xunit;

namespace UseCase.Test.User.Register;
public class RegisterUserUseCaseTest
{

    private static RegisterUserUseCase CreateUseCase(string? email= null)
    {
        var mapper = MapperBuilder.Build();
        var passwordEncrypter = PasswordEncrypterBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var readRepository = new UserReadOnlyRepositoryBuilder();
        var writeRepository = new UserWriteOnlyRepositoryBuilder().Build();
        var accessToken = JwtTokenGeneratorBuilder.Build();
        
        if (string.IsNullOrEmpty(email) == false) 
            readRepository.ExistActiveUserWithEmail(email);

        return new RegisterUserUseCase(writeRepository, readRepository.Build(), unitOfWork, passwordEncrypter, mapper, accessToken);
    }

    [Fact]
    public async void Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.ShouldNotBeNull();
        result.Name.ShouldSatisfyAllConditions(result =>
        {
            result.ShouldNotBeEmpty();
            result.ShouldNotBeNull();
        });
        result.Tokens.ShouldNotBeNull();
        result.Tokens.AccessToken.ShouldNotBeEmpty();
    }
}

