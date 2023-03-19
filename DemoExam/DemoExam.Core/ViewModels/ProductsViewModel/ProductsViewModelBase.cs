﻿using System.Windows.Input;
using DemoExam.Core.Models;
using DemoExam.Core.ObservableObjects;
using DemoExam.Core.Services.ViewModelServices.Products;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels.ProductsViewModel;

public abstract class ProductsViewModelBase : MvxViewModel<User>
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
    
    public ICommand ChangeSortOrderCommand => new MvxCommand(ChangeSortOrder);

    public ICommand CloseCommand =>
        new MvxAsyncCommand( () => NavigationService.Close(this));

    public ICommand OpenOrderCommand =>
        new MvxAsyncCommand(async () =>
        {
            await NavigationService.Navigate<OrderViewModel, User>(User).ConfigureAwait(false);
            await RaisePropertyChanged(nameof(CanOpenOrder)).ConfigureAwait(false);
        });

    public bool CanOpenOrder => ViewModelService.CanOpenOrder();

    public string CurrentSelectionAmount => $"{Products?.Count ?? 0}/{_allProducts?.Count() ?? 0}";

    public string SearchString
    {
        get => _searchString;
        set
        {
            SetProperty(ref _searchString, value);
            UpdateProductsSelection();
        }
    }

    public User User { get; set; }

    public ObservableProduct? SelectedProduct
    {
        get => _selectedProduct;
        set => SetProperty(ref _selectedProduct, value);
    }
    
    protected readonly IMvxNavigationService NavigationService;
    protected readonly IProductsViewModelService ViewModelService;

    private Func<double, bool> _discountSelectorPredicate = _ => true;
    private string _searchString;
    private ObservableProduct? _selectedProduct;
    private SortOrder _sortOrder;
    private string _sortOrderName;
    private IEnumerable<ObservableProduct>? _allProducts;

    public ProductsViewModelBase(IMvxNavigationService navigationService,
        IProductsViewModelService viewModelService)
    {
        NavigationService = navigationService;
        ViewModelService = viewModelService;
        SortOrderName = DetermineSortOrderName();
    }

    public override async Task Initialize()
    {
        _allProducts = await ViewModelService.GetAllProducts().ConfigureAwait(false);
        Products = new(_allProducts);
        await RaisePropertyChanged(nameof(Products)).ConfigureAwait(false);
        await RaisePropertyChanged(nameof(CurrentSelectionAmount)).ConfigureAwait(false);
    }

    /*
    
    */
    
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

        UpdateProductsSelection();
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
        UpdateProductsSelection();
    }
    
    protected async Task UpdateProducts()
    {
        _allProducts = await ViewModelService.GetAllProducts().ConfigureAwait(false);
        UpdateProductsSelection();
    }

    private async void UpdateProductsSelection()
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