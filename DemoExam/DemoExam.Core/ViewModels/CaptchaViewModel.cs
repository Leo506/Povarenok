using System.Windows.Input;
using DemoExam.Core.Utils;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class CaptchaViewModel : MvxViewModel<Action<bool>>
{
    private string _captchaText;
    public string CaptchaText
    {
        get => _captchaText;
        set => SetProperty(ref _captchaText, value);
    }

    private string _userInput;
    public string UserInput
    {
        get => _userInput;
        set => SetProperty(ref _userInput, value);
    }

    private MvxCommand? _verifyCommand;
    public ICommand VerifyCommand => _verifyCommand ??= new MvxCommand(async () =>
    {
        await _navigationService.Close(this).ConfigureAwait(false);
        _callBack(UserInput == CaptchaText);
    });

    private Action<bool> _callBack;

    private readonly IMvxNavigationService _navigationService;
    
    public CaptchaViewModel(IMvxNavigationService navigationService)
    {
        _navigationService = navigationService;
        CaptchaText = CaptchaTextGenerator.GenerateCaptchaText();
    }

    public override void Prepare(Action<bool> parameter)
    {
        _callBack = parameter;
    }
}