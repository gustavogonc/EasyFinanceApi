using EasyFinance.Communication.Request;
using EasyFinance.Exceptions;
using Shouldly;
using System.Net;
using System.Text.Json;
using Xunit;

namespace WebApi.Test.Login.DoLogin;
public class DoLoginTest : EasyFinanceClassFixture
{
    private readonly string url = "login";
    private readonly string _email;
    private readonly string _password;
    private readonly string _name;

    public DoLoginTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _email = factory.GetEmail(); 
        _password = factory.GetPassword();
        _name = factory.GetName();
    }

    [Fact]
    public async Task Success()
    {
        var request = new RequestLoginJson
        {
            Email = _email,
            Password = _password,
        };

        var response = await DoPost(url: url, request: request);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().ShouldBe(_name);
        responseData.RootElement.GetProperty("tokens").GetProperty("accessToken").GetString().ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Error_Password_Invalid()
    {
        var request = new RequestLoginJson
        {
            Email = _email,
            Password = string.Empty,
        };

        var response = await DoPost(url: url, request: request);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID");

        errors.ShouldHaveSingleItem().ShouldSatisfyAllConditions(error => error.GetString()?.ShouldContain(expectedMessage!));
    }

    [Fact]
    public async Task Error_Email_Invalid()
    {
        var request = new RequestLoginJson
        {
            Email = string.Empty,
            Password = _password,
        };

        var response = await DoPost(url: url, request: request);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageException.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID");

        errors.ShouldHaveSingleItem().ShouldSatisfyAllConditions(error => error.GetString()?.ShouldContain(expectedMessage!));
    }
}

