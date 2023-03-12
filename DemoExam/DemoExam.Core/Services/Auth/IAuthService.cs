using DemoExam.Core.Models;

namespace DemoExam.Core.Services.Auth;

public interface IAuthService
{
    Task<UserModel> AuthenticateAsync(string login, string password);
}