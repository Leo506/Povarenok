using System.Windows.Input;
using DemoExam.Core.Services.Alert;
using DemoExam.Core.Services.Products;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class ProductEditViewModel : MvxViewModel<Product>
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

    public Product Product { get; private set; }

    public ICommand CloseCommand => new MvxAsyncCommand(CloseAndSave);

    private async Task CloseAndSave()
    {
        try
        {
            await _productsService.UpdateProduct(Product);
            await _navigationService.Close(this);
        }
        catch (Exception e)
        {
            _alert.Alert(e.GetType().Name, e.Message);
        }
    }

    public override void Prepare(Product parameter)
    {
        Product = parameter;
    }

    public void UpdateProductPhoto(byte[] bytes)
    {
        Product.ProductPhoto = bytes;
    }
}