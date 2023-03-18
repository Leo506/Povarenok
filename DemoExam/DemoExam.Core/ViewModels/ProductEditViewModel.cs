using System.Windows.Input;
using DemoExam.Core.ObservableObjects;
using DemoExam.Core.Services.Alert;
using DemoExam.Core.Services.ProductEditService;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class ProductEditViewModel : MvxViewModel<ObservableProduct>
{
    private readonly IProductEditService _editService;
    private readonly IMvxNavigationService _navigationService;
    private IAlert _alert;
    private IMvxAsyncCommand? _closeCommand;


    public ProductEditViewModel(IMvxNavigationService navigationService, IProductEditService editService, IAlert alert)
    {
        _navigationService = navigationService;
        _editService = editService;
        _alert = alert;
    }

    public ObservableProduct ObservableProduct { get; private set; }

    public ICommand CloseCommand =>
        _closeCommand ??= new MvxAsyncCommand(CloseAndSave);

    private async Task CloseAndSave()
    {
        try
        {
            await _editService.SaveProduct(ObservableProduct.Product);
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