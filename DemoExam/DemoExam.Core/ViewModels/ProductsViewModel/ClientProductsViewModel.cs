using System.Windows.Input;
using DemoExam.Core.Services.Products;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace DemoExam.Core.ViewModels.ProductsViewModel;

public class ClientProductsViewModel : ProductsViewModelBase
{
    public ICommand AddProductToOrderCommand => new MvxAsyncCommand(AddProductToOrder);

    public ClientProductsViewModel(IMvxNavigationService navigationService,
        IProductsService productsService) : base(navigationService, productsService)
    {
    }
    
    private async Task AddProductToOrder()
    {
        if (SelectedProduct is null) return;
        
        await ProductsService.AddProductToOrder(SelectedProduct.Product);
        await RaisePropertyChanged(nameof(CanOpenOrder)).ConfigureAwait(false);
    }
}