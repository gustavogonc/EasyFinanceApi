using CommonTestUtilities.Requests;
using EasyFinance.Application.UseCases.User.Register;
using EasyFinance.Exceptions;
using Shouldly;
using Xunit;

namespace Validator.Test.User.Register;
public class RegisterUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Error_Name_Empty()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();

        request.Name = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.Select(error => error.ErrorMessage).ShouldContain(ResourceMessageException.NAME_EMPTY);
    }

    [Fact]
    public void Error_Empty_Email()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();

        request.Email = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.Select(error => error.ErrorMessage).ShouldContain(ResourceMessageException.EMAIL_EMPTY);
    }

    [Fact]
    public void Error_Invalid_Email()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();

        request.Email = request.Name;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.Select(error => error.ErrorMessage).ShouldContain(ResourceMessageException.EMAIL_INVALID);
    }

    [Fact]
    public void Error_Empty_Password()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();

        request.Password = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.Select(error => error.ErrorMessage).ShouldContain(ResourceMessageException.PASSWORD_EMPTY);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Error_Invalid_Password_Length(int passwordLength)
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build(passwordLength);

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.Select(error => error.ErrorMessage).ShouldContain(ResourceMessageException.MINIMUM_PASSWORD_LENGTH);
    }
}

