using DemoExam.Core.Contexts;
using DemoExam.Core.Services.Auth;
using DemoExam.Core.Services.ProductEditService;
using DemoExam.Core.Services.Products;
using DemoExam.Core.Services.UserRole;
using DemoExam.Core.Services.ViewModelServices;
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
        Mvx.IoCProvider.RegisterType<IProductsService, ProductsService>();
        Mvx.IoCProvider.RegisterType<IUserRoleService, UserRoleService>();
        Mvx.IoCProvider.RegisterType<IProductEditService, ProductEditService>();
        Mvx.IoCProvider.RegisterType<IProductsViewModelService, ProductsViewModelService>();

        RegisterAppStart<AuthViewModel>();

        base.Initialize();
    }
}