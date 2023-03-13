using System.Windows.Input;
using DemoExam.Core.ObservableObjects;
using DemoExam.Core.Services.ProductEditService;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class ProductEditViewModel : MvxViewModel<ObservableProduct>
{
    private readonly IProductEditService _editService;

    private readonly IMvxNavigationService _navigationService;
    private IMvxAsyncCommand? _closeCommand;


    public ProductEditViewModel(IMvxNavigationService navigationService, IProductEditService editService)
    {
        _navigationService = navigationService;
        _editService = editService;
    }

    public ObservableProduct ObservableProduct { get; private set; }

    public ICommand CloseCommand =>
        _closeCommand ??= new MvxAsyncCommand(CloseAndSave);

    private async Task CloseAndSave()
    {
        await _editService.SaveProduct(ObservableProduct);
        await _navigationService.Close(this);
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