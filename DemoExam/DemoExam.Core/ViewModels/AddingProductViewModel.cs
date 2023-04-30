using System.Windows.Input;
using DemoExam.Core.ObservableObjects;
using DemoExam.Core.Services.Alert;
using DemoExam.Core.Services.ViewModelServices.AddingProduct;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class AddingProductViewModel : MvxViewModel
{
    private readonly IAlert _alert;


    private readonly IMvxNavigationService _navigationService;
    private readonly IAddingProductViewModelService _viewModelService;

    public AddingProductViewModel(IMvxNavigationService navigationService,
        IAddingProductViewModelService viewModelService, IAlert alert)
    {
        _navigationService = navigationService;
        _viewModelService = viewModelService;
        _alert = alert;
    }

    public ObservableProduct ObservableProduct { get; set; } = new(new Product());
    public ICommand SaveAndCloseCommand => new MvxAsyncCommand(SaveAndClose);

    private async Task SaveAndClose()
    {
        try
        {
            await _viewModelService.AddProductAsync(ObservableProduct.Product).ConfigureAwait(false);
            await _navigationService.Close(this).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            _alert.Alert(e.GetType().Name, e.Message);
        }
    }

    public void SetProductPhoto(byte[] bytes)
    {
        ObservableProduct.ProductPhoto = bytes;
    }
}