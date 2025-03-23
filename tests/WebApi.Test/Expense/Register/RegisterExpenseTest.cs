using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using EasyFinance.Exceptions;
using Shouldly;
using System.Net;
using System.Text.Json;
using Xunit;

namespace WebApi.Test.Expense.Register;
public class RegisterExpenseTest : EasyFinanceClassFixture
{
    private readonly string url = "expense";
    private readonly Guid _userIdentifier;
    public RegisterExpenseTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPost(url: url, request: request, token: token);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Error_Empty_Title()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = string.Empty;

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPost(url: url, request: request, token: token);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.ShouldHaveSingleItem().ShouldSatisfyAllConditions(error => error.GetString()?.ShouldContain(ResourceMessageException.EXPENSE_EMPTY_TITLE));
    }

    [Fact]
    public async Task Error_Zero_Value()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Value = 0;

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPost(url: url, request: request, token: token);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.ShouldHaveSingleItem().ShouldSatisfyAllConditions(error => error.GetString()?.ShouldContain(ResourceMessageException.EXPENSE_GREATHER_THAN_ZERO));
    }    
    
    [Fact]
    public async Task Error_Zero_Months()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Months = 0;

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPost(url: url, request: request, token: token);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.ShouldHaveSingleItem().ShouldSatisfyAllConditions(error => error.GetString()?.ShouldContain(ResourceMessageException.EXPENSE_MONTHS_GREATHER_THAN_ZERO));
    }    
    
    [Fact]
    public async Task Error_Null_Type()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Type = null;

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPost(url: url, request: request, token: token);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.ShouldHaveSingleItem().ShouldSatisfyAllConditions(error => error.GetString()?.ShouldContain(ResourceMessageException.EXPENSE_TYPE_EMPTY));
    }
    
    [Fact]
    public async Task Error_Null_PaymentMethod()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.PaymentMethod = null;

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPost(url: url, request: request, token: token);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.ShouldHaveSingleItem().ShouldSatisfyAllConditions(error => error.GetString()?.ShouldContain(ResourceMessageException.EXPENSE_PAYMENT_METHOD_EMPTY));
    }    
    
    [Fact]
    public async Task Error_Null_Category()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Category = null;

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPost(url: url, request: request, token: token);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.ShouldHaveSingleItem().ShouldSatisfyAllConditions(error => error.GetString()?.ShouldContain(ResourceMessageException.EXPENSE_CATEGORY_EMPTY));
    }
}

