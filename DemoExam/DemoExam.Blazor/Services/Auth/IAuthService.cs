using DemoExam.Blazor.ViewModels;

namespace DemoExam.Blazor.Services.Auth;

public interface IAuthService
{
    Task<bool> LoginAsync(string login, string password);
    
    Task Logout();
    Task Registrate(RegistrationViewModel registrationViewModel);
}