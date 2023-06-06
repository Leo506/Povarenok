using DemoExam.Core.Services.Auth;
using DemoExam.Core.Services.Categories;
using DemoExam.Core.Services.Manufacturer;
using DemoExam.Core.Services.Order;
using DemoExam.Core.Services.Orders;
using DemoExam.Core.Services.Products;
using DemoExam.Core.Services.Suppliers;
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
        Mvx.IoCProvider.RegisterType<IManufacturerService, ManufacturerService>();
        Mvx.IoCProvider.RegisterType<ISupplierService, SupplierService>();
        Mvx.IoCProvider.RegisterType<ICategoriesService, CategoriesService>();

        RegisterAppStart<AuthViewModel>();

        base.Initialize();
    }
}