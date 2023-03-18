using DemoExam.Core.Services.ViewModelServices.Products;
using MvvmCross.Navigation;

namespace DemoExam.Core.ViewModels;

public class ManagerProductsViewModel : ProductsViewModelBase
{
    public ManagerProductsViewModel(IMvxNavigationService navigationService, IProductsViewModelService viewModelService)
        : base(navigationService, viewModelService)
    {
    }
}