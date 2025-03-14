using CommonTestUtilities.Requests;
using EasyFinance.Exceptions;
using Shouldly;
using System.Net;
using System.Text.Json;
using Xunit;

namespace WebApi.Test.User.Register;
public class RegisterUserTest : EasyFinanceClassFixture
{
    private readonly string url = "user";

    public RegisterUserTest(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var response = await DoPost(url: url, request: request);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().ShouldNotBeNullOrWhiteSpace();
        responseData.RootElement.GetProperty("tokens").GetProperty("accessToken").GetString().ShouldNotBeNullOrWhiteSpace();
    }


    [Fact]
    public async Task Error_Empty_Name()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        request.Name = string.Empty;

        var response = await DoPost(url: url, request: request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.ShouldHaveSingleItem().ShouldSatisfyAllConditions(error => error.GetString()?.ShouldContain(ResourceMessageException.NAME_EMPTY));
    }

    [Fact]
    public async Task Error_Empty_Email()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        request.Email = string.Empty;

        var response = await DoPost(url: url, request: request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.ShouldHaveSingleItem().ShouldSatisfyAllConditions(error => error.GetString()?.ShouldContain(ResourceMessageException.EMAIL_EMPTY));
    }

    [Fact]
    public async Task Error_Invalid_Email()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        request.Email = request.Name;

        var response = await DoPost(url: url, request: request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.ShouldHaveSingleItem().ShouldSatisfyAllConditions(error => error.GetString()?.ShouldContain(ResourceMessageException.EMAIL_INVALID));
    }

    [Fact]
    public async Task Error_Empty_Password()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        request.Password = string.Empty;

        var response = await DoPost(url: url, request: request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.ShouldHaveSingleItem().ShouldSatisfyAllConditions(error => error.GetString()?.ShouldContain(ResourceMessageException.PASSWORD_EMPTY)); ;
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    public async Task Error_Invalid_Length_Password(int passwordLength)
    {
        var request = RequestRegisterUserJsonBuilder.Build(passwordLength);

        var response = await DoPost(url: url, request: request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.ShouldHaveSingleItem().ShouldSatisfyAllConditions(error => error.GetString()?.ShouldContain(ResourceMessageException.MINIMUM_PASSWORD_LENGTH));
    }
}

