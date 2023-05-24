using System.Windows.Input;
using DemoExam.Core.ObservableObjects;
using DemoExam.Core.Services.Alert;
using DemoExam.Core.Services.Products;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class ProductEditViewModel : MvxViewModel<ObservableProduct>
{
    private readonly IProductsService _productsService;
    private readonly IMvxNavigationService _navigationService;
    private readonly IAlert _alert;

    public ProductEditViewModel(IMvxNavigationService navigationService, IProductsService productsService, IAlert alert)
    {
        _navigationService = navigationService;
        _productsService = productsService;
        _alert = alert;
    }

    public ObservableProduct ObservableProduct { get; private set; }

    public ICommand CloseCommand => new MvxAsyncCommand(CloseAndSave);

    private async Task CloseAndSave()
    {
        try
        {
            await _productsService.UpdateProduct(ObservableProduct.Product);
            await _navigationService.Close(this);
        }
        catch (Exception e)
        {
            _alert.Alert(e.GetType().Name, e.Message);
        }
    }

    public override void Prepare(ObservableProduct parameter)
    {
        ObservableProduct = parameter;
    }

    public void UpdateProductPhoto(byte[] bytes)
    {
        ObservableProduct.ProductPhoto = bytes;
    }
}