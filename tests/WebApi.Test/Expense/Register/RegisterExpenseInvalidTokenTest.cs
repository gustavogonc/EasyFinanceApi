using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using Shouldly;
using System.Net;
using Xunit;

namespace WebApi.Test.Expense.Register;
public class RegisterExpenseInvalidTokenTest : EasyFinanceClassFixture
{
    private readonly string url = "expense";

    public RegisterExpenseInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Error_Empty_Token()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();

        var response = await DoPost(url: url, request: request, token: string.Empty);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Invalid_Token()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();

        var response = await DoPost(url: url, request: request, token: "tokenInvalid");

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Token_With_User_Not_Found()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();

        var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

        var response = await DoPost(url: url, request: request, token);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}

