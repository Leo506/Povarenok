using DemoExam.Core.Contexts;
using DemoExam.Core.Services;
using DemoExam.Core.Services.Auth;
using DemoExam.Core.ViewModels;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace DemoExam.Core;

public class App : MvxApplication
{
    public override void Initialize()
    {
        Mvx.IoCProvider.LazyConstructAndRegisterSingleton(() => new TradeContext());
        Mvx.IoCProvider.RegisterType<IAuthService, AuthService>();
        
        RegisterAppStart<AuthViewModel>();
        
        base.Initialize();
    }
}