using DemoExam.Blazor.ViewModels;

namespace DemoExam.Blazor.Services.Auth;

public interface IAuthService
{
    Task Logout();
    Task Registrate(RegistrationViewModel registrationViewModel);
}