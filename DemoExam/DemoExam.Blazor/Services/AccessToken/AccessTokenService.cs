using DemoExam.Blazor.Services.LocalStorage;

namespace DemoExam.Blazor.Services.AccessToken;

public class AccessTokenService : IAccessTokenService
{
    private const string AccessTokenKey = "access_token";
    private readonly ILocalStorageService _localStorageService;

    public AccessTokenService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public Task<string?> GetAccessToken() => _localStorageService.GetAsync<string>(AccessTokenKey);

    public Task SetAccessToken(string accessToken) => _localStorageService.SetAsync(AccessTokenKey, accessToken);
    
    public Task RemoveAccessToken() => _localStorageService.RemoveAsync(AccessTokenKey);
}