using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using EasyFinance.Application.UseCases.User.Register;
using EasyFinance.Exceptions;
using EasyFinance.Exceptions.ExceptionBase;
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

    [Fact]
    public async void Error_Empty_Name() 
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        request.Name = string.Empty;

        var useCase = CreateUseCase();

        Func<Task> act = async() => await useCase.Execute(request);
         var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.GetErrorMessages().Count.ShouldBe(1);
        exception.GetErrorMessages().ShouldContain(ResourceMessageException.NAME_EMPTY);
    }


    [Fact]
    public async void Error_Empty_Email()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        request.Email = string.Empty;

        var useCase = CreateUseCase();

        Func<Task> act = async () => await useCase.Execute(request);
        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.GetErrorMessages().Count.ShouldBe(1);
        exception.GetErrorMessages().ShouldContain(ResourceMessageException.EMAIL_EMPTY);
    }

    [Fact]
    public async void Error_Invalid_Email()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        request.Email = request.Name;

        var useCase = CreateUseCase();

        Func<Task> act = async () => await useCase.Execute(request);
        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.GetErrorMessages().Count.ShouldBe(1);
        exception.GetErrorMessages().ShouldContain(ResourceMessageException.EMAIL_INVALID);
    }

    [Fact]
    public async void Error_Empty_Password()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        request.Password = string.Empty;

        var useCase = CreateUseCase();

        Func<Task> act = async() => await useCase.Execute(request);
        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.GetErrorMessages().Count.ShouldBe(1);
        exception.GetErrorMessages().ShouldContain(ResourceMessageException.PASSWORD_EMPTY);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    public async void Error_Invalid_Length_Password(int passwordLength)
    {
        var request = RequestRegisterUserJsonBuilder.Build(passwordLength);

        var useCase = CreateUseCase();

        Func<Task> act = async () => await useCase.Execute(request);
        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.GetErrorMessages().Count.ShouldBe(1);
        exception.GetErrorMessages().ShouldContain(ResourceMessageException.MINIMUM_PASSWORD_LENGTH);
    }
}

