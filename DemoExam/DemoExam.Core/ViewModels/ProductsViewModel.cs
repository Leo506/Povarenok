using System.Windows.Input;
using DemoExam.Core.Models;
using DemoExam.Core.Services.Products;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

internal enum SortOrder
{
    NONE,
    ASC,
    DESC
}

public class ProductsViewModel : MvxViewModel
{
    public string SortOrderName
    {
        get => _sortOrderName;
        set
        {
            SetProperty(ref _sortOrderName, value);
            RaisePropertyChanged(() => SortOrderName);
        }
    }

    public MvxObservableCollection<Product> Products { get; set; }

    public ICommand ChangeSortOrderCommand => _changeSortOrderCommand ??= new MvxCommand(ChangeSortOrder);

    public string SearchString
    {
        get => _searchString;
        set
        {
            SetProperty(ref _searchString, value);
            UpdateProducts();
        }
    }

    private string _sortOrderName;
    private MvxCommand _changeSortOrderCommand;
    private string _searchString;
    private readonly IProductsService _productsService;
    private SortOrder _sortOrder;
    
    public ProductsViewModel(IProductsService productsService)
    {
        _productsService = productsService;
        Products = new MvxObservableCollection<Product>(_productsService.GetAll());
        SortOrderName = DetermineSortOrderName();
    }

    private void ChangeSortOrder()
    {
        _sortOrder = _sortOrder switch
        {
            SortOrder.NONE => SortOrder.ASC,
            SortOrder.DESC => SortOrder.ASC,
            SortOrder.ASC => SortOrder.DESC,
            _ => SortOrder.ASC
        };

        SortOrderName = DetermineSortOrderName();
        
        UpdateProducts();
    }
    
    private void UpdateProducts()
    {
        SearchProducts();
        SortProducts();
        RaisePropertyChanged(() => Products);
    }

    private void SearchProducts()
    {
        if (string.IsNullOrEmpty(SearchString))
            Products = new MvxObservableCollection<Product>(_productsService.GetAll());
        else
            Products = new MvxObservableCollection<Product>(_productsService.GetWhere(x =>
                x.ProductName.Contains(SearchString)));
    }
    
    private void SortProducts()
    {
        Products = _sortOrder switch
        {
            SortOrder.ASC => new MvxObservableCollection<Product>(Products.OrderBy(x => x.ProductCost)),
            SortOrder.DESC => new MvxObservableCollection<Product>(Products.OrderByDescending(x => x.ProductCost)),
            _ => Products
        };
    }

    private string DetermineSortOrderName()
    {
        return _sortOrder switch
        {
            SortOrder.ASC => "Ascending sorting",
            SortOrder.DESC => "Descending sorting",
            _ => "No sorting"
        };
    }
}
