using DemoExam.Core.Services;
using DemoExam.Core.Services.Auth;
using MvvmCross;
using MvvmCross.ViewModels;

namespace DemoExam.Core;

public class App : MvxApplication
{
    public override void Initialize()
    {
        Mvx.IoCProvider.RegisterType<IAuthService, AuthService>();
        
        base.Initialize();
    }
}