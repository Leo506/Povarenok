using System.Net;
using System.Net.Http.Json;
using DemoExam.Blazor.Exceptions;
using DemoExam.Blazor.Services.AccessToken;
using DemoExam.Blazor.Shared;
using DemoExam.Blazor.ViewModels;

namespace DemoExam.Blazor.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IAccessTokenService _accessTokenService;
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient, IAccessTokenService accessTokenService)
    {
        _httpClient = httpClient;
        _accessTokenService = accessTokenService;
    }

    public async Task<bool> LoginAsync(string login, string password)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "auth/login");
            request.Headers.Add("login", login);
            request.Headers.Add("password", password);
            var response = await _httpClient.SendAsync(request)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var accessToken = await response.Content.ReadAsStringAsync();
            await _accessTokenService.SetAccessToken(accessToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public Task Logout() => _accessTokenService.RemoveAccessToken();

    public async Task Registrate(RegistrationViewModel registrationViewModel)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "Auth/registration");
        var dto = new UserDto(registrationViewModel.UserName, registrationViewModel.UserSurname,
            registrationViewModel.UserPatronymic, registrationViewModel.Login, registrationViewModel.Password);
        request.Content = JsonContent.Create(dto);
        var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        if (response.StatusCode == HttpStatusCode.Conflict)
            throw new DuplicateLoginException();
    }
}