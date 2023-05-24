using DemoExam.Core.Services.Auth;
using DemoExam.Core.Services.Order;
using DemoExam.Core.Services.Orders;
using DemoExam.Core.Services.Products;
using DemoExam.Core.Services.ViewModelServices.Order;
using DemoExam.Core.ViewModels;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace DemoExam.Core;

public class App : MvxApplication
{
    public override void Initialize()
    {
        
        Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IOrderService, OrderService>();
        Mvx.IoCProvider.RegisterType<IAuthService, AuthService>();
        Mvx.IoCProvider.RegisterType<IProductsService, ProductsService>();
        Mvx.IoCProvider.RegisterType<IOrderViewModelService, OrderViewModelService>();
        Mvx.IoCProvider.RegisterType<IOrdersService, OrdersService>();

        RegisterAppStart<AuthViewModel>();

        base.Initialize();
    }
}