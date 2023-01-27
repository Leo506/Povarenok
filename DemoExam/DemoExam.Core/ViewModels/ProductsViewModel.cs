using DemoExam.Core.Contexts;
using DemoExam.Core.Models;
using MvvmCross.ViewModels;

namespace DemoExam.Core.ViewModels;

public class ProductsViewModel : MvxViewModel
{
    public MvxObservableCollection<Product> Products { get; private set; }
    
    public ProductsViewModel(TradeContext context)
    {
        Products = new MvxObservableCollection<Product>(context.Products);
    }
}