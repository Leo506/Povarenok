using DemoExam.Core.Contexts;
using DemoExam.Core.Models;
using DemoExam.Core.ObservableObjects;

namespace DemoExam.Core.Services.Products;

public class ProductsService : IProductsService
{
    private readonly TradeContext _tradeContext;

    public ProductsService(TradeContext tradeContext)
    {
        _tradeContext = tradeContext;
    }

    public IEnumerable<ObservableProduct> GetAll()
    {
        return _tradeContext.Products.Select(product => new ObservableProduct(product));
    }

    public IEnumerable<ObservableProduct> GetWhere(Func<Product, bool> predicate)
    {
        return _tradeContext.Products.Where(predicate).Select(product => new ObservableProduct(product)).ToList();
    }

    public int Count()
    {
        return _tradeContext.Products.Count();
    }

    public void DeleteProduct(ObservableProduct observableProduct)
    {
        _tradeContext.Products.Remove(observableProduct.Product);
        _tradeContext.SaveChanges();
    }
}