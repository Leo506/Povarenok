using DemoExam.Core.Contexts;
using DemoExam.Core.Models;
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

    private TradeContext _context;
    
    public ProductsViewModel(TradeContext context)
    {
        _context = context;
        Products = new MvxObservableCollection<Product>(context.Products);
    }

    private void UpdateProducts()
    {
        if (string.IsNullOrEmpty(SearchString))
            Products = new MvxObservableCollection<Product>(_context.Products);
        else
            Products = new MvxObservableCollection<Product>(Products.Where(x => x.ProductName.Contains(SearchString)));
    }
}