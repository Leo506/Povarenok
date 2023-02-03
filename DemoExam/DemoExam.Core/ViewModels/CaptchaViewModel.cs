using System.Windows.Input;
using DemoExam.Core.Utils;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class CaptchaViewModel : MvxViewModel<Action<bool>>
{
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

    private string _captchaText;
    private string _userInput;
    private MvxCommand? _verifyCommand;
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