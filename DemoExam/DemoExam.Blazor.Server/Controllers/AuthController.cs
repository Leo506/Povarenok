using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using DemoExam.Blazor.Shared.Dto.Requests;
using DemoExam.Blazor.Shared.Dto.Responses;
using DemoExam.Domain.Exceptions;
using DemoExam.Domain.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DemoExam.Blazor.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpGet("login")]
    public async Task<IActionResult> Login([FromHeader] string login, [FromHeader] string password)
    {
        try
        {
            var user = await _authService.AuthenticateAsync(login, password).ConfigureAwait(false);
            var encodedJwt = GenerateJWTToken(user);
            return Ok(new Login(encodedJwt));
        }
        catch (AuthenticationException)
        {
            _logger.LogInformation("Failed to authenticate user");
            return Unauthorized();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to authenticate user");
            return BadRequest();
        }
    }

    private static string GenerateJWTToken(Domain.Models.User user)
    {
        var now = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            notBefore: now,
            claims: GetUserClaims(user),
            expires: now.Add(AuthOptions.LifeTime),
            signingCredentials: new SigningCredentials(AuthOptions.SecurityKey, SecurityAlgorithms.HmacSha256)
        );
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        return encodedJwt;
    }

    private static IEnumerable<Claim> GetUserClaims(Domain.Models.User user) =>
        new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, user.Name),
            new(ClaimsIdentity.DefaultRoleClaimType, user.UserRoleNavigation.Name),
            new("UserId", user.Id.ToString())
        };

    [HttpPost("registration")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        try
        {
            var (userName, userSurname, userPatronymic, login, password) = user;
            await _authService.RegisterUserAsync(login, password, userName, userSurname, userPatronymic)
                .ConfigureAwait(false);
            return Ok();
        }
        catch (DuplicateLoginException)
        {
            return Conflict();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}