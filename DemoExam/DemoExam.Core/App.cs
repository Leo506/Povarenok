using DemoExam.Core.Services.ViewModelServices.Order;
using DemoExam.Core.ViewModels;
using DemoExam.Domain.Services.Auth;
using DemoExam.Domain.Services.Categories;
using DemoExam.Domain.Services.Manufacturer;
using DemoExam.Domain.Services.Order;
using DemoExam.Domain.Services.Orders;
using DemoExam.Domain.Services.Products;
using DemoExam.Domain.Services.Suppliers;
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