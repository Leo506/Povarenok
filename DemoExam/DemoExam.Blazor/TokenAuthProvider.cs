using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DemoExam.Blazor.Services.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace DemoExam.Blazor;

public class TokenAuthProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;

    public TokenAuthProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var accessToken = await _localStorage.GetAsync<string>("access_token");
        if (accessToken is null)
            return CreateAnonymous();

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(accessToken);

        var identity = new ClaimsIdentity(jwt.Claims, "Bearer");
        var principal = new ClaimsPrincipal(identity);

        return new AuthenticationState(principal);
    }

    private static AuthenticationState CreateAnonymous()
    {
        var identity = new ClaimsIdentity();
        var principal = new ClaimsPrincipal(identity);
        return new AuthenticationState(principal);
    }
}