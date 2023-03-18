using System.Windows.Input;
using DemoExam.Core.ObservableObjects;
using DemoExam.Core.Services.ViewModelServices.Products;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace DemoExam.Core.ViewModels;

public class ClientProductsViewModel : ProductsViewModelBase
{
    public ICommand AddProductToOrderCommand => new MvxAsyncCommand<ObservableProduct>(AddProductToOrder);

    public ClientProductsViewModel(IMvxNavigationService navigationService,
        IProductsViewModelService viewModelService) : base(navigationService, viewModelService)
    {
    }
    
    private async Task AddProductToOrder(ObservableProduct product)
    {
        await ViewModelService.AddProductToOrder(product);
        await RaisePropertyChanged(nameof(CanOpenOrder)).ConfigureAwait(false);
    }
}