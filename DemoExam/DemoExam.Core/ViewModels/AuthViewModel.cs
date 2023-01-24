using System.Text.Json;
using System.Windows.Input;
using DemoExam.Core.Services;
using DemoExam.Core.Services.Alert;
using DemoExam.Core.Services.Auth;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class AuthViewModel : MvxViewModel
{
    private string _login = default!;
    public string Login
    {
        get => _login;
        set => SetProperty(ref _login, value);
    }

    private string _password = default!;
    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    private MvxCommand? _authCommand;
    public ICommand AuthCommand => _authCommand ??= new MvxCommand(async () =>
    {
        try
        {
            var user = await _authService.AuthenticateAsync(Login, Password);
            _alert.Alert("",
                $"Name: {user.UserSurname} {user.UserName} {user.UserPatronymic}\nRole: {user.UserRoleNavigation.RoleName}");
        }
        catch (Exception e)
        {
            _alert.Alert("", e.Message);
        }
    });

    private readonly IAuthService _authService;
    private readonly IAlert _alert;
    
    public AuthViewModel(IAuthService authService, IAlert alert)
    {
        _authService = authService;
        _alert = alert;
    }
}