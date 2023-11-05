using System.Net.Http.Json;
using DemoExam.Blazor.Services.AccessToken;
using DemoExam.Blazor.Services.Basket;
using DemoExam.Blazor.Shared;

namespace DemoExam.Blazor.Services.Orders;

public class OrdersService : IOrdersService
{
    private readonly IAccessTokenService _accessTokenService;
    private readonly HttpClient _httpClient;
    private readonly IBasketService _basketService;

    public OrdersService(HttpClient httpClient, IAccessTokenService accessTokenService, IBasketService basketService)
    {
        _httpClient = httpClient;
        _accessTokenService = accessTokenService;
        _basketService = basketService;
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

    public async Task<OrderDto> GetOrder(int orderId)
    {
        var accessToken = await _accessTokenService.GetAccessToken();
        var request = new HttpRequestMessage(HttpMethod.Get, $"Orders/{orderId}");
        request.Headers.Add("Authorization", $"Bearer {accessToken}");
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<OrderDto>() ?? new();
    }

    public async Task CreateNew(int pickupPointId)
    {
        var accessToken = await _accessTokenService.GetAccessToken();
        var request = new HttpRequestMessage(HttpMethod.Post, "Orders");
        request.Headers.Add("Authorization", $"Bearer {accessToken}");
        var dto = new NewOrderDto()
        {
            PickupPointId = pickupPointId,
            Products = _basketService.GetAll().ToDictionary(x => x.Key.ProductArticleNumber, x => x.Value)
        };
        request.Content = JsonContent.Create(dto);
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}