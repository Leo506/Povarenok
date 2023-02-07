using System.Windows.Input;
using DemoExam.Core.Models;
using DemoExam.Core.Services.ProductEditService;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class ProductEditViewModel : MvxViewModel<Product>
{
    public Product Product { get; private set; }

    public ICommand CloseCommand =>
        _closeCommand ??= new MvxAsyncCommand(CloseAndSave);

    private readonly IMvxNavigationService _navigationService;
    private readonly IProductEditService _editService;
    private IMvxAsyncCommand? _closeCommand;
    
    
    public ProductEditViewModel(IMvxNavigationService navigationService, IProductEditService editService)
    {
        _navigationService = navigationService;
        _editService = editService;
    }

    private async Task CloseAndSave()
    {
        await _editService.SaveProduct(Product);
        await _navigationService.Close(this);
    }
    
    public override void Prepare(Product parameter)
    {
        Product = parameter;
    }
}