using System.Windows.Input;
using DemoExam.Core.Extensions;
using DemoExam.Core.Models;
using DemoExam.Core.NotifyObjects;
using DemoExam.Core.Services.Alert;
using DemoExam.Core.Services.ViewModelServices.Products;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public enum SortOrder
{
    None,
    Asc,
    Desc
}

public class ProductsViewModel : MvxViewModel<User>
{
    private readonly IAlert _alert;
    private readonly IMvxNavigationService _navigationService;
    private readonly IProductsViewModelService _viewModelService;
    private MvxCommand? _changeSortOrderCommand;
    private MvxAsyncCommand? _closeCommand;
    private Func<double, bool> _discountSelectorPredicate = _ => true;
    private string _searchString;
    private ProductNotifyObject? _selectedProduct;
    private SortOrder _sortOrder;
    private string _sortOrderName;

    public ProductsViewModel(IMvxNavigationService navigationService, IAlert alert,
        IProductsViewModelService viewModelService)
    {
        _navigationService = navigationService;
        _alert = alert;
        _viewModelService = viewModelService;
        Products = new MvxObservableCollection<ProductNotifyObject>(_viewModelService.GetAllProducts());
        SortOrderName = DetermineSortOrderName();
        _selectedProduct = Products.FirstOrDefault();
    }

    public string SortOrderName
    {
        get => _sortOrderName;
        set => SetProperty(ref _sortOrderName, value);
    }

    public MvxObservableCollection<ProductNotifyObject> Products { get; set; }

    public List<ProductOperation> AvailableProductOperations => GetAvailableOperationsForUser();

    public ICommand ChangeSortOrderCommand => _changeSortOrderCommand ??= new MvxCommand(ChangeSortOrder);

    public ICommand CloseCommand =>
        _closeCommand ??= new MvxAsyncCommand(async () => await _navigationService.Close(this));

    public ICommand OpenOrderCommand =>
        new MvxAsyncCommand(async () => await _navigationService.Navigate<OrderViewModel, User>(User));

    public bool CanOpenOrder => _viewModelService.CanOpenOrder();

    public string CurrentSelectionAmount => $"{Products.Count}/{_viewModelService.GetProductsCount()}";

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

    public ProductNotifyObject? SelectedProduct
    {
        get => _selectedProduct;
        set => SetProperty(ref _selectedProduct, value);
    }

    private List<ProductOperation> GetAvailableOperationsForUser()
    {
        var list = new List<ProductOperation>
        {
            new("Add To Order", new MvxCommand<ProductNotifyObject>(product =>
            {
                _viewModelService.AddProductToOrder(product);
                RaisePropertyChanged(nameof(CanOpenOrder));
            }))
        };

        if (!User.IsAdmin()) return list;

        list.Add(new ProductOperation("Edit product",
            new MvxCommand<ProductNotifyObject>(product =>
                _navigationService.Navigate<ProductEditViewModel, ProductNotifyObject>(product))));
        list.Add(new ProductOperation("Remove product", new MvxCommand<ProductNotifyObject>(product =>
        {
            var choice = _alert.ShowChoice("Deleting product", "Do you shure?");
            if (choice is not ChoiceResult.Positive) return;
            _viewModelService.DeleteProduct(product);
            UpdateProducts();
        })));
        list.Add(new ProductOperation("Add new product",
            new MvxCommand<ProductNotifyObject>(product => _navigationService.Navigate<AddingProductViewModel>())));

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

    private void UpdateProducts()
    {
        Products = new MvxObservableCollection<ProductNotifyObject>(
            _viewModelService.SelectProducts(SearchString, _sortOrder, _discountSelectorPredicate));
        RaisePropertyChanged(() => Products);
        RaisePropertyChanged(() => CurrentSelectionAmount);
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