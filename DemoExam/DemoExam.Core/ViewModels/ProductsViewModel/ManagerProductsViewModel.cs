using System.Windows.Input;
using DemoExam.Core.Services.ViewModelServices.Products;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace DemoExam.Core.ViewModels.ProductsViewModel;

public class ManagerProductsViewModel : ClientProductsViewModel
{
    public ICommand OpenOrdersPageCommand => new MvxAsyncCommand(() => NavigationService.Navigate<OrdersViewModel>());
    
    public ManagerProductsViewModel(IMvxNavigationService navigationService, IProductsViewModelService viewModelService)
        : base(navigationService, viewModelService)
    {
    }
}