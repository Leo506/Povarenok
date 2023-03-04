using System.Windows.Input;
using DemoExam.Core.Models;
using DemoExam.Core.Services.Auth;
using DemoExam.Core.Services.Order;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class AuthViewModel : MvxViewModel
{
    private readonly IAuthService _authService;
    private readonly IMvxNavigationService _navigationService;
    private readonly IOrderService _orderService;
    private MvxAsyncCommand? _authCommand;
    private MvxAsyncCommand? _continueAsGuestCommand;

    private bool _isLoginAvailable = true;

    private string _login = default!;
    private string _loginButtonText = "Login";
    private string _password = default!;

    public AuthViewModel(IAuthService authService, IMvxNavigationService navigationService, IOrderService orderService)
    {
        _authService = authService;
        _navigationService = navigationService;
        _orderService = orderService;
    }

    public string Login
    {
        get => _login;
        set => SetProperty(ref _login, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public string LoginButtonText
    {
        get => _loginButtonText;
        set => SetProperty(ref _loginButtonText, value);
    }

    public ICommand AuthCommand => _authCommand ??= new MvxAsyncCommand(Authenticate, () => _isLoginAvailable);

    public ICommand ContinueAsGuest => _continueAsGuestCommand ??= new MvxAsyncCommand(LoginAsGuest);

    private async Task Authenticate()
    {
        try
        {
            var user = await _authService.AuthenticateAsync(Login, Password).ConfigureAwait(false);
            _orderService.CreateNewOrder();
            await _navigationService.Navigate<ProductsViewModel, User>(user);
        }
        catch (Exception e)
        {
            await _navigationService.Navigate<CaptchaViewModel, Action<bool>>(CaptchaCallback)
                .ConfigureAwait(false);
        }

        void CaptchaCallback(bool result)
        {
            if (result) return;

            _isLoginAvailable = false;
            _authCommand?.RaiseCanExecuteChanged();
            Task.Run(async () =>
            {
                var waitTime = DateTime.Now;
                for (var i = 0; i < 10; i++)
                {
                    LoginButtonText = (10 - (int)(DateTime.Now - waitTime).TotalSeconds).ToString();
                    await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
                }

                _isLoginAvailable = true;
                LoginButtonText = "Login";
                _authCommand?.RaiseCanExecuteChanged();
            });
        }
    }

    private async Task LoginAsGuest()
    {
        _orderService.CreateNewOrder();
        await _navigationService.Navigate<ProductsViewModel, User>(User.Guest);
    }
}