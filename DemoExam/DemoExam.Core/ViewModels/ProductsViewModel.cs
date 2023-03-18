using System.Windows.Input;
using DemoExam.Core.Extensions;
using DemoExam.Core.Models;
using DemoExam.Core.ObservableObjects;
using DemoExam.Core.Services.Alert;
using DemoExam.Core.Services.ViewModelServices.Products;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class ProductsViewModel : MvxViewModel<User>
{
    private enum SortOrder
    {
        None,
        Asc,
        Desc
    }
    
    public string SortOrderName
    {
        get => _sortOrderName;
        set => SetProperty(ref _sortOrderName, value);
    }

    public MvxObservableCollection<ObservableProduct>? Products { get; set; }

    public List<ProductOperation> AvailableProductOperations => GetAvailableOperationsForUser();

    public ICommand ChangeSortOrderCommand => new MvxCommand(ChangeSortOrder);

    public ICommand CloseCommand =>
        new MvxAsyncCommand( () => _navigationService.Close(this));

    public ICommand OpenOrderCommand =>
        new MvxAsyncCommand(async () =>
        {
            await _navigationService.Navigate<OrderViewModel, User>(User).ConfigureAwait(false);
            await RaisePropertyChanged(nameof(CanOpenOrder)).ConfigureAwait(false);
        });

    public bool CanOpenOrder => _viewModelService.CanOpenOrder();

    public string CurrentSelectionAmount => $"{Products?.Count ?? 0}/{_allProducts?.Count() ?? 0}";

    public string SearchString
    {
        get => _searchString;
        set
        {
            SetProperty(ref _searchString, value);
            SelectProducts();
        }
    }

    public User User { get; set; }

    public ObservableProduct? SelectedProduct
    {
        get => _selectedProduct;
        set => SetProperty(ref _selectedProduct, value);
    }
    
    private readonly IAlert _alert;
    private readonly IMvxNavigationService _navigationService;
    private readonly IProductsViewModelService _viewModelService;
    private Func<double, bool> _discountSelectorPredicate = _ => true;
    private string _searchString;
    private ObservableProduct? _selectedProduct;
    private SortOrder _sortOrder;
    private string _sortOrderName;
    private IEnumerable<ObservableProduct>? _allProducts;

    public ProductsViewModel(IMvxNavigationService navigationService, IAlert alert,
        IProductsViewModelService viewModelService)
    {
        _navigationService = navigationService;
        _alert = alert;
        _viewModelService = viewModelService;
        SortOrderName = DetermineSortOrderName();
    }

    public override async Task Initialize()
    {
        _allProducts = await _viewModelService.GetAllProducts().ConfigureAwait(false);
        Products = new(_allProducts);
        await RaisePropertyChanged(nameof(Products)).ConfigureAwait(false);
        await RaisePropertyChanged(nameof(CurrentSelectionAmount)).ConfigureAwait(false);
    }

    private List<ProductOperation> GetAvailableOperationsForUser()
    {
        var list = new List<ProductOperation>
        {
            new("Add To Order", new MvxAsyncCommand<ObservableProduct>(AddProductToOrder))
        };

        if (!User.IsAdmin()) return list;

        list.Add(new ProductOperation("Edit product",new MvxAsyncCommand<ObservableProduct>(OpenEditProductDialog)));
        list.Add(new ProductOperation("Remove product",new MvxAsyncCommand<ObservableProduct>(DeleteProduct)));
        list.Add(new ProductOperation("Add new product",new MvxAsyncCommand<ObservableProduct>(OpenAddingProductDialog)));

        return list;
    }
    
    private async Task AddProductToOrder(ObservableProduct product)
    {
        await _viewModelService.AddProductToOrder(product);
        await RaisePropertyChanged(nameof(CanOpenOrder)).ConfigureAwait(false);
    }
    
    private async Task OpenEditProductDialog(ObservableProduct product)
    {
        await _navigationService.Navigate<ProductEditViewModel, ObservableProduct>(product);
        await RaisePropertyChanged(nameof(Products)).ConfigureAwait(false);
    }
    
    private async Task DeleteProduct(ObservableProduct product)
    {
        var choice = _alert.ShowChoice("Deleting product", "Do you shure?");
        if (choice is ChoiceResult.Negative) return;
        await _viewModelService.DeleteProduct(product).ConfigureAwait(false);
        _allProducts = await _viewModelService.GetAllProducts();
        SelectProducts();
    }

    private async Task OpenAddingProductDialog(ObservableProduct product)
    {
        await _navigationService.Navigate<AddingProductViewModel>().ConfigureAwait(false);
        _allProducts = await _viewModelService.GetAllProducts().ConfigureAwait(false);
        SelectProducts();
    }
    
    private void ChangeSortOrder()
    {
        _sortOrder = _sortOrder switch
        {
            SortOrder.None => SortOrder.Asc,
            SortOrder.Desc => SortOrder.Asc,
            SortOrder.Asc => SortOrder.Desc,
            _ => SortOrder.Asc
        };

        SortOrderName = DetermineSortOrderName();

        SelectProducts();
    }
    
    private string DetermineSortOrderName()
    {
        return _sortOrder switch
        {
            SortOrder.Asc => "Ascending sorting",
            SortOrder.Desc => "Descending sorting",
            _ => "No sorting"
        };
    }

    public void ChangeDiscountSelector(Func<double, bool> discountSelectorPredicate)
    {
        _discountSelectorPredicate = discountSelectorPredicate;
        SelectProducts();
    }

    private async void SelectProducts()
    {
        if (string.IsNullOrEmpty(SearchString))
            Products = new(_allProducts);
        else
            Products = new(_allProducts.Where(p =>
                p.ProductName.ToLowerInvariant().Contains(SearchString.ToLowerInvariant())));

        Products = new(Products.Where(p => _discountSelectorPredicate(p.CurrentDiscount)));

        Products = _sortOrder switch
        {
            SortOrder.Asc => new(Products.OrderBy(p => p.ProductCostWithDiscount)),
            SortOrder.Desc => new(Products.OrderByDescending(p => p.ProductCostWithDiscount)),
            _ => Products
        };
        
        await RaisePropertyChanged(nameof(Products)).ConfigureAwait(false);
        await RaisePropertyChanged(nameof(CurrentSelectionAmount)).ConfigureAwait(false);
    }
    
    public override void Prepare(User parameter)
    {
        User = parameter;
    }
}