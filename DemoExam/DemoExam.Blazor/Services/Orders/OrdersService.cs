using System.Net.Http.Json;
using DemoExam.Blazor.Services.AccessToken;
using DemoExam.Blazor.Shared;

namespace DemoExam.Blazor.Services.Orders;

public class OrdersService : IOrdersService
{
    private readonly IAccessTokenService _accessTokenService;
    private readonly HttpClient _httpClient;

    public OrdersService(HttpClient httpClient, IAccessTokenService accessTokenService)
    {
        _httpClient = httpClient;
        _accessTokenService = accessTokenService;
    }

    public async Task<List<OrderShortDto>> GetUserOrders(int userId)
    {
        var accessToken = await _accessTokenService.GetAccessToken();
        var request = new HttpRequestMessage(HttpMethod.Get, $"/Orders/User/{userId}");
        request.Headers.Add("Authorization", $"Bearer {accessToken}");
        var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<OrderShortDto>>() ?? new();
    }
}