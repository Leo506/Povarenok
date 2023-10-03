using DemoExam.Domain.Models;

namespace DemoExam.Domain.Services.Auth;

public interface IAuthService
{
    Task<User> AuthenticateAsync(string login, string password);
}