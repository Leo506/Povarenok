using System.Net.Http.Json;
using DemoExam.Blazor.Services.AccessToken;
using DemoExam.Blazor.Shared;

namespace DemoExam.Blazor.Services.PickupPoints;

public class PickupPointsService : IPickupPointsService
{
    private readonly IAccessTokenService _accessTokenService;
    private readonly HttpClient _httpClient;

    public PickupPointsService(IAccessTokenService accessTokenService, HttpClient httpClient)
    {
        _accessTokenService = accessTokenService;
        _httpClient = httpClient;
    }

    public async Task<List<PickupPointDto>> GetAll()
    {
        try
        {
            var accessToken = await _accessTokenService.GetAccessToken();
            var request = new HttpRequestMessage(HttpMethod.Get, "/pickupPoints");
            request.Headers.Add("Authorization", accessToken);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<PickupPointDto>>() ?? new();
        }
        catch (Exception)
        {
            return new();
        }
    }
}