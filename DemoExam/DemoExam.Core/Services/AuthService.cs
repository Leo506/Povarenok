using System.Security.Authentication;
using DemoExam.Core.Contexts;
using DemoExam.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Core.Services;

public class AuthService : IAuthService
{
    private readonly TradeContext _context;

    public AuthService(TradeContext context)
    {
        _context = context;
    }

    public async Task<User> AuthenticateAsync(string login, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserLogin == login && x.UserPassword == password);
        if (user is null)
            throw new AuthenticationException("Incorrect login or password");

        user.UserRoleNavigation = await _context.Roles.FirstOrDefaultAsync(x => x.RoleId == user.UserRole) ??
                                  throw new InvalidDataException("User has no role");

        return user;
    }
}