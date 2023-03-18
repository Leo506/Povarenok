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
            await _navigationService.Navigate<OrderViewModel, User>(User);
            RaisePropertyChanged(nameof(CanOpenOrder));
        });

    public bool CanOpenOrder => _viewModelService.CanOpenOrder();

    public string CurrentSelectionAmount => $"{Products?.Count ?? 0}/{_allProducts?.Count() ?? 0}";

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
        RaisePropertyChanged(nameof(Products));
        RaisePropertyChanged(nameof(CurrentSelectionAmount));
    }

    private List<ProductOperation> GetAvailableOperationsForUser()
    {
        var list = new List<ProductOperation>
        {
            new("Add To Order", new MvxCommand<ObservableProduct>(product =>
            {
                _viewModelService.AddProductToOrder(product);
                RaisePropertyChanged(nameof(CanOpenOrder));
            }))
        };

        if (!User.IsAdmin()) return list;

        list.Add(new ProductOperation("Edit product",
            new MvxCommand<ObservableProduct>(product =>
                _navigationService.Navigate<ProductEditViewModel, ObservableProduct>(product))));
        list.Add(new ProductOperation("Remove product", new MvxCommand<ObservableProduct>(product =>
        {
            var choice = _alert.ShowChoice("Deleting product", "Do you shure?");
            if (choice is not ChoiceResult.Positive) return;
            _viewModelService.DeleteProduct(product);
            UpdateProducts();
        })));
        list.Add(new ProductOperation("Add new product",
            new MvxCommand<ObservableProduct>(product => _navigationService.Navigate<AddingProductViewModel>())));

        return list;
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

        UpdateProducts();
    }

    public void ChangeDiscountSelector(Func<double, bool> discountSelectorPredicate)
    {
        _discountSelectorPredicate = discountSelectorPredicate;
        UpdateProducts();
    }

    private async void UpdateProducts()
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
        
        RaisePropertyChanged(nameof(Products));
        RaisePropertyChanged(nameof(CurrentSelectionAmount));
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

    public override void Prepare(User parameter)
    {
        User = parameter;
    }
}