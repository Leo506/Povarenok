using Arbus.Network;
using DemoExam.Blazor.Network.Endpoints;
using DemoExam.Blazor.Services.AccessToken;
using DemoExam.Blazor.Shared.Dto.Requests;
using DemoExam.Blazor.ViewModels;

namespace DemoExam.Blazor.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IAccessTokenService _accessTokenService;
    private readonly HttpClientContext _clientContext;

    public AuthService(IAccessTokenService accessTokenService, HttpClientContext clientContext)
    {
        _accessTokenService = accessTokenService;
        _clientContext = clientContext;
    }

    public async Task<bool> LoginAsync(string login, string password)
    {
        try
        {
            var loginResponse = await _clientContext.RunEndpoint(new LoginEndpoint(login, password));
            await _accessTokenService.SetAccessToken(loginResponse.AccessToken);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public Task Logout() => _accessTokenService.RemoveAccessToken();

    public async Task Registrate(RegistrationViewModel registrationViewModel)
    {
        var dto = new User(registrationViewModel.UserName, registrationViewModel.UserSurname,
            registrationViewModel.UserPatronymic, registrationViewModel.Login, registrationViewModel.Password);
        await _clientContext.RunEndpoint(new RegistrationEndpoint(dto));
    }
}