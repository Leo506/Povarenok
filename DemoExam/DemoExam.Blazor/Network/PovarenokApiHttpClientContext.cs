using System.Net.Http.Headers;
using Arbus.Network;
using DemoExam.Blazor.Services.AccessToken;

namespace DemoExam.Blazor.Network;

public class PovarenokApiHttpClientContext : HttpClientContext
{
    private readonly IAccessTokenService _accessTokenService;
    private string? _accessToken;

    public PovarenokApiHttpClientContext(
        INativeHttpClient httpClientHandler, 
        IAccessTokenService accessTokenService) 
        : base(httpClientHandler)
    {
        _accessTokenService = accessTokenService;
    }

    public override async Task<HttpResponseMessage> RunEndpointInternal(ApiEndpoint endpoint)
    {
        _accessToken = await _accessTokenService.GetAccessToken();
        return await base.RunEndpointInternal(endpoint);
    }

    protected override void AddHeaders(HttpRequestHeaders headers)
    {
        base.AddHeaders(headers);
        headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
    }

    public override Uri? GetBaseUri() => new("http://localhost:5062");
}