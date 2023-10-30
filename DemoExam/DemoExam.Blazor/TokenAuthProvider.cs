using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DemoExam.Blazor.Services.AccessToken;
using Microsoft.AspNetCore.Components.Authorization;

namespace DemoExam.Blazor;

public class TokenAuthProvider : AuthenticationStateProvider
{
    private readonly IAccessTokenService _accessTokenService;

    public TokenAuthProvider(IAccessTokenService accessTokenService)
    {
        _accessTokenService = accessTokenService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var accessToken = await _accessTokenService.GetAccessToken();
        if (accessToken is null)
            return CreateAnonymous();

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(accessToken);

        if (DateTime.UtcNow >= jwt.ValidTo)
            return CreateAnonymous();
        
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