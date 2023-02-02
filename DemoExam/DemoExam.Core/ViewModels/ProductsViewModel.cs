using DemoExam.Core.Contexts;
using DemoExam.Core.Models;
using DemoExam.Core.Services.Products;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class ProductsViewModel : MvxViewModel
{
    public MvxObservableCollection<Product> Products { get; set; }

    private string _searchString;

    public string SearchString
    {
        get => _searchString;
        set
        {
            SetProperty(ref _searchString, value);
            UpdateProducts();
            RaisePropertyChanged(() => Products);
        }
    }

    private readonly IProductsService _productsService;
    
    public ProductsViewModel(IProductsService productsService)
    {
        _productsService = productsService;
        Products = new MvxObservableCollection<Product>(_productsService.GetAll());
    }

    private void UpdateProducts()
    {
        if (string.IsNullOrEmpty(SearchString))
            Products = new MvxObservableCollection<Product>(_productsService.GetAll());
        else
            Products = new MvxObservableCollection<Product>(_productsService.GetWhere(x =>
                x.ProductName.Contains(SearchString)));
    }
}