using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using DemoExam.Blazor.Shared;
using DemoExam.Domain.Exceptions;
using DemoExam.Domain.Models;
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
            return Ok(encodedJwt);
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

    private static string GenerateJWTToken(User user)
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

    private static IEnumerable<Claim> GetUserClaims(User user) =>
        new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, user.UserName),
            new(ClaimsIdentity.DefaultRoleClaimType, user.UserRoleNavigation.RoleName)
        };

    [HttpPost("registration")]
    public async Task<IActionResult> Register([FromBody] UserDto userDto)
    {
        try
        {
            var (userName, userSurname, userPatronymic, login, password) = userDto;
            await _authService.RegisterUser(login, password, userName, userSurname, userPatronymic)
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