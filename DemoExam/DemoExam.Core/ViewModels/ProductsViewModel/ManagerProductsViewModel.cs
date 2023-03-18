using DemoExam.Core.Services.ViewModelServices.Products;
using MvvmCross.Navigation;

namespace DemoExam.Core.ViewModels.ProductsViewModel;

public class ManagerProductsViewModel : ClientProductsViewModel
{
    public ManagerProductsViewModel(IMvxNavigationService navigationService, IProductsViewModelService viewModelService)
        : base(navigationService, viewModelService)
    {
    }
}