using System.Windows.Input;
using DemoExam.Core.Services.ViewModelServices.Products;
using DemoExam.Translation;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace DemoExam.Core.ViewModels.ProductsViewModel;

public class ClientProductsViewModel : ProductsViewModelBase
{
    public ICommand AddProductToOrderCommand => new MvxAsyncCommand(AddProductToOrder);

    public string AddProductToOrderText => Translate.AddToOrder;

    public ClientProductsViewModel(IMvxNavigationService navigationService,
        IProductsViewModelService viewModelService) : base(navigationService, viewModelService)
    {
    }
    
    private async Task AddProductToOrder()
    {
        if (SelectedProduct is null) return;
        
        await ViewModelService.AddProductToOrder(SelectedProduct);
        await RaisePropertyChanged(nameof(CanOpenOrder)).ConfigureAwait(false);
    }
}