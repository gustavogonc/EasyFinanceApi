using System.Net.Http.Json;
using Xunit;

namespace WebApi.Test;
public class EasyFinanceClassFixture : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public EasyFinanceClassFixture(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();

    protected async Task<HttpResponseMessage> DoPost(string url, object request)
    {
        return await _httpClient.PostAsJsonAsync(url, request);
    }
}


