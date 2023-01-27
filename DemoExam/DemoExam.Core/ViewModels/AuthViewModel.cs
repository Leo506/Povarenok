using System.Text.Json;
using System.Windows.Input;
using DemoExam.Core.Services;
using DemoExam.Core.Services.Alert;
using DemoExam.Core.Services.Auth;
using MvvmCross.Commands;
using MvvmCross.Navigation;
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

    private string _loginButtonText = "Login";
    public string LoginButtonText
    {
        get => _loginButtonText;
        set => SetProperty(ref _loginButtonText, value);
    }

    private MvxAsyncCommand? _authCommand;
    public ICommand AuthCommand => _authCommand ??= new MvxAsyncCommand(Authenticate, () => _isLoginAvailable);

    private readonly IAuthService _authService;
    private readonly IAlert _alert;
    private readonly IMvxNavigationService _navigationService;

    private bool _isLoginAvailable = true;
    
    public AuthViewModel(IAuthService authService, IAlert alert, IMvxNavigationService navigationService)
    {
        _authService = authService;
        _alert = alert;
        _navigationService = navigationService;
    }

    private async Task Authenticate()
    {
        try
        {
            var user = await _authService.AuthenticateAsync(Login, Password);
            _alert.Alert("",
                $"Name: {user.UserSurname} {user.UserName} {user.UserPatronymic}\nRole: {user.UserRoleNavigation.RoleName}");
        }
        catch (Exception e)
        {
            await _navigationService.Navigate<CaptchaViewModel, Action<bool>>(CaptchaCallback)
                .ConfigureAwait(false);
        }

        void CaptchaCallback(bool result)
        {
            if (result is false)
            {
                _isLoginAvailable = false;
                _authCommand?.RaiseCanExecuteChanged();
                Task.Run(async () =>
                {
                    var waitTime = DateTime.Now;
                    for (var i = 0; i < 10; i++)
                    {
                        LoginButtonText = (10 - (int)(DateTime.Now - waitTime).TotalSeconds).ToString();
                        await Task.Delay(TimeSpan.FromSeconds(1));
                    }
                    
                    _isLoginAvailable = true;
                    LoginButtonText = "Login";
                    _authCommand?.RaiseCanExecuteChanged();
                });
            }
        }
    }
}