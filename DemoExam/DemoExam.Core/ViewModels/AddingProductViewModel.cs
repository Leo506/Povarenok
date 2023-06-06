using System.Windows.Input;
using DemoExam.Core.Services.Alert;
using DemoExam.Core.Services.Categories;
using DemoExam.Core.Services.Manufacturer;
using DemoExam.Core.Services.Products;
using DemoExam.Core.Services.Suppliers;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class AddingProductViewModel : MvxViewModel
{
    private readonly IAlert _alert;
    private readonly IMvxNavigationService _navigationService;
    private readonly IProductsService _productsService;
    private readonly IManufacturerService _manufacturerService;
    private readonly ISupplierService _supplierService;
    private readonly ICategoriesService _categoriesService;

    public Product Product { get; set; } = new();
    
    public ICommand SaveAndCloseCommand => new MvxAsyncCommand(SaveAndClose);
    public List<Manufacturer> Manufacturers { get; private set; }
    
    public List<Supplier> Suppliers { get; private set; }
    
    public List<string> Categories { get; private set; }

    public string ManufacturerName { get; set; } = string.Empty;

    public string SupplierName { get; set; } = string.Empty;
    
    public AddingProductViewModel(IMvxNavigationService navigationService,
        IProductsService productsService, IAlert alert, IManufacturerService manufacturerService, ISupplierService supplierService, ICategoriesService categoriesService)
    {
        _navigationService = navigationService;
        _productsService = productsService;
        _alert = alert;
        _manufacturerService = manufacturerService;
        _supplierService = supplierService;
        _categoriesService = categoriesService;
    }

    public override async Task Initialize()
    {
        Manufacturers = (await _manufacturerService.GetAll().ConfigureAwait(false)).ToList();
        Suppliers = (await _supplierService.GetAll().ConfigureAwait(false)).ToList();
        Categories = (await _categoriesService.GetAll().ConfigureAwait(false)).ToList();
        await RaisePropertyChanged(nameof(Manufacturers));
        await RaisePropertyChanged(nameof(Suppliers));
        await RaisePropertyChanged(nameof(Categories));
    }

    private async Task SaveAndClose()
    {
        try
        {
            Product.ManufacturerName = ManufacturerName;
            Product.SupplierName = SupplierName;
            
            await _productsService.AddProduct(Product).ConfigureAwait(false);
            await _navigationService.Close(this).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            _alert.Alert(e.GetType().Name, e.Message);
        }
    }

    public void SetProductPhoto(byte[] bytes)
    {
        Product.ProductPhoto = bytes;
    }
}