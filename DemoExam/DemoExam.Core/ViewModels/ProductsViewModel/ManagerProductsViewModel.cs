using System.Windows.Input;
using DemoExam.Domain.Services.Products;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace DemoExam.Core.ViewModels.ProductsViewModel;

public class ManagerProductsViewModel : ClientProductsViewModel
{
    public ICommand OpenOrdersPageCommand => new MvxAsyncCommand(() => NavigationService.Navigate<OrdersViewModel>());
    
    public ManagerProductsViewModel(IMvxNavigationService navigationService, IProductsService productsService)
        : base(navigationService, productsService)
    {
    }
}