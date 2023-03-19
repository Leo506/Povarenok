using DemoExam.Core.Services.Auth;
using DemoExam.Core.Services.Order;
using DemoExam.Core.Services.Products;
using DemoExam.Core.Services.ViewModelServices.AddingProduct;
using DemoExam.Core.Services.ViewModelServices.Order;
using DemoExam.Core.Services.ViewModelServices.Products;
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
        Mvx.IoCProvider.RegisterType<IProductsViewModelService, ProductsViewModelService>();
        Mvx.IoCProvider.RegisterType<IAddingProductViewModelService, AddingProductViewModelService>();
        Mvx.IoCProvider.RegisterType<IOrderViewModelService, OrderViewModelService>();

        RegisterAppStart<AuthViewModel>();

        base.Initialize();
    }
}