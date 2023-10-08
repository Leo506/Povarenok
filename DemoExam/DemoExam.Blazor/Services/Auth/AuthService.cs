using DemoExam.Blazor.Services.LocalStorage;

namespace DemoExam.Blazor.Services.Auth;

public class AuthService : IAuthService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient;

    public AuthService(ILocalStorageService localStorageService, HttpClient httpClient)
    {
        _localStorageService = localStorageService;
        _httpClient = httpClient;
    }

    public async Task<bool> LoginAsync(string login, string password)
    {
        try
        {
            var accessToken = await _httpClient.GetStringAsync($"/login?login={login}&password={password}")
                .ConfigureAwait(false);
            await _localStorageService.SetAsync("access_token", accessToken).ConfigureAwait(false);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public Task Logout() => _localStorageService.RemoveAsync("access_token");
}