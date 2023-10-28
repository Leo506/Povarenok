using System.Net;
using System.Net.Http.Json;
using DemoExam.Blazor.Exceptions;
using DemoExam.Blazor.Services.LocalStorage;
using DemoExam.Blazor.Shared;
using DemoExam.Blazor.ViewModels;

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
            var request = new HttpRequestMessage(HttpMethod.Get, "auth/login");
            request.Headers.Add("login", login);
            request.Headers.Add("password", password);
            var response = await _httpClient.SendAsync(request)
                .ConfigureAwait(false);
            var accessToken = await response.Content.ReadAsStringAsync();
            await _localStorageService.SetAsync("access_token", accessToken).ConfigureAwait(false);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public Task Logout() => _localStorageService.RemoveAsync("access_token");

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