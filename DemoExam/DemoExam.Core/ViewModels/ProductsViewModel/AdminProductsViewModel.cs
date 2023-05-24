using System.Windows.Input;
using DemoExam.Core.Services.Alert;
using DemoExam.Core.Services.Products;
using DemoExam.Translation;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace DemoExam.Core.ViewModels.ProductsViewModel;

public class AdminProductsViewModel : ManagerProductsViewModel
{
    private readonly IAlert _alert;

    public ICommand OpenEditProductDialogCommand => new MvxAsyncCommand(OpenEditProductDialog);

    public ICommand OpenAddProductDialogCommand => new MvxAsyncCommand(OpenAddProductDialog);

    public ICommand DeleteProductCommand => new MvxAsyncCommand(DeleteProduct);

    public AdminProductsViewModel(IMvxNavigationService navigationService, IProductsService productsService,
        IAlert alert) :
        base(navigationService, productsService)
    {
        _alert = alert;
    }

    private async Task OpenEditProductDialog()
    {
        if (SelectedProduct is null) return;
        
        await NavigationService.Navigate<ProductEditViewModel, Product>(SelectedProduct);
        await UpdateProducts().ConfigureAwait(false);
    }
    
    private async Task OpenAddProductDialog()
    {
        if (SelectedProduct is null) return;
        
        await NavigationService.Navigate<AddingProductViewModel>().ConfigureAwait(false);
        await UpdateProducts().ConfigureAwait(false);
    }
    
    private async Task DeleteProduct()
    {
        if (SelectedProduct is null) return;
        
        var choice = _alert.ShowChoice(Translate.DeleteProduct, Translate.AreYouSure);
        if (choice is ChoiceResult.Negative) return;
        await ProductsService.DeleteProduct(SelectedProduct).ConfigureAwait(false);
        await UpdateProducts().ConfigureAwait(false);
    }
}