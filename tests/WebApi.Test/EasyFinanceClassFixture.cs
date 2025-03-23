using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit;

namespace WebApi.Test;
public class EasyFinanceClassFixture : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public EasyFinanceClassFixture(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();

    protected async Task<HttpResponseMessage> DoPost(string url, object request, string token = "")
    {
        AuthorizeRequest(token);
        return await _httpClient.PostAsJsonAsync(url, request);
    }

    private void AuthorizeRequest(string token)
    {
        if (string.IsNullOrEmpty(token))
            return;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}


