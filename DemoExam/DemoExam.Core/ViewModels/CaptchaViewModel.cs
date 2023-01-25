using System.Windows.Input;
using DemoExam.Core.Utils;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class CaptchaViewModel : MvxViewModel
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
    public ICommand VerifyCommand => _verifyCommand ??= new MvxCommand(() =>
    {
        if (UserInput == CaptchaText)
        {
            // TODO on success captcha pass
        }
    });

    public CaptchaViewModel()
    {
        CaptchaText = CaptchaTextGenerator.GenerateCaptchaText();
    }
}