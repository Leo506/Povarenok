using DemoExam.Core.Models;

namespace DemoExam.Core.Services;

public interface IAuthService
{
    Task<User> AuthenticateAsync(string login, string password);
}