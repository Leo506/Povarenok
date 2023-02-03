using System.Windows.Input;
using DemoExam.Core.Models;
using DemoExam.Core.Services.Alert;
using DemoExam.Core.Services.Products;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

internal enum SortOrder
{
    NONE,
    ASC,
    DESC
}

public class ProductsViewModel : MvxViewModel<User>
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
    
    public List<ProductOperation> AvailableProductOperations => _productsService.GetAvailableProductsOperationsForUser(User);

    public ICommand ChangeSortOrderCommand => _changeSortOrderCommand ??= new MvxCommand(ChangeSortOrder);

    public ICommand CloseCommand =>
        _closeCommand ??= new MvxAsyncCommand(async () => await _navigationService.Close(this));

    public string CurrentSelectionAmount => $"{Products.Count}/{_productsService.Count()}";
    
    public string SearchString
    {
        get => _searchString;
        set
        {
            SetProperty(ref _searchString, value);
            UpdateProducts();
        }
    }
    
    public User User { get; set; }

    public Product? SelectedProduct
    {
        get => _selectedProduct;
        set
        {
            SetProperty(ref _selectedProduct, value);
            RaisePropertyChanged(() => SelectedProduct);
        }
    }

    private readonly IProductsService _productsService;
    private readonly IMvxNavigationService _navigationService;
    private readonly IAlert _alert;
    private string _sortOrderName;
    private MvxCommand? _changeSortOrderCommand;
    private MvxAsyncCommand? _closeCommand;
    private Func<double, bool> _discountSelectorPredicate = _ => true;
    private string _searchString;
    private SortOrder _sortOrder;
    private Product? _selectedProduct;
    
    public ProductsViewModel(IProductsService productsService, IMvxNavigationService navigationService, IAlert alert)
    {
        _productsService = productsService;
        _navigationService = navigationService;
        _alert = alert;
        Products = new MvxObservableCollection<Product>(_productsService.GetAll());
        SortOrderName = DetermineSortOrderName();
        _selectedProduct = Products.FirstOrDefault();
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
        SelectByDiscount();
        SortProducts();
        RaisePropertyChanged(() => Products);
        RaisePropertyChanged(() => CurrentSelectionAmount);
    }


    private void SearchProducts()
    {
        if (string.IsNullOrEmpty(SearchString))
            Products = new MvxObservableCollection<Product>(_productsService.GetAll());
        else
            Products = new MvxObservableCollection<Product>(_productsService.GetWhere(x =>
                x.ProductName.Contains(SearchString)));
    }
    
    private void SelectByDiscount()
    {
        Products = new MvxObservableCollection<Product>(Products.Where(x =>
            _discountSelectorPredicate(x.CurrentDiscount)));
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
    
    public void ChangeDiscountSelector(Func<double, bool> discountSelectorPredicate)
    {
        _discountSelectorPredicate = discountSelectorPredicate;
        UpdateProducts();
    }

    public override void Prepare(User parameter)
    {
        User = parameter;
    }
}
