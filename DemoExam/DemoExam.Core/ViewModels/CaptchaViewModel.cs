using System.Windows.Input;
using DemoExam.Core.Utils;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class CaptchaViewModel : MvxViewModel<Action<bool>>
{
    private readonly IMvxNavigationService _navigationService;
    private Action<bool> _callBack;

    private string _captchaText;
    private string _userInput;
    private MvxCommand? _verifyCommand;

    public CaptchaViewModel(IMvxNavigationService navigationService)
    {
        _navigationService = navigationService;
        CaptchaText = CaptchaTextGenerator.GenerateCaptchaText();
    }

    public string CaptchaText
    {
        get => _captchaText;
        set => SetProperty(ref _captchaText, value);
    }

    public string UserInput
    {
        get => _userInput;
        set => SetProperty(ref _userInput, value);
    }

    public ICommand VerifyCommand => _verifyCommand ??= new MvxCommand(async () =>
    {
        await _navigationService.Close(this).ConfigureAwait(false);
        _callBack(UserInput == CaptchaText);
    });

    public override void Prepare(Action<bool> parameter)
    {
        _callBack = parameter;
    }
}