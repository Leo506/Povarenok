using DemoExam.Core.Contexts;
using DemoExam.Core.Models;
using DemoExam.Core.NotifyObjects;

namespace DemoExam.Core.Services.Products;

public class ProductsService : IProductsService
{
    private readonly TradeContext _tradeContext;

    public ProductsService(TradeContext tradeContext)
    {
        _tradeContext = tradeContext;
    }

    public IEnumerable<ProductNotifyObject> GetAll()
    {
        return _tradeContext.Products.Select(product => new ProductNotifyObject(product));
    }

    public IEnumerable<ProductNotifyObject> GetWhere(Func<Product, bool> predicate)
    {
        return _tradeContext.Products.Where(predicate).Select(product => new ProductNotifyObject(product)).ToList();
    }

    public int Count()
    {
        return _tradeContext.Products.Count();
    }

    public void DeleteProduct(ProductNotifyObject product)
    {
        _tradeContext.Products.Remove(product.Product);
        _tradeContext.SaveChanges();
    }
}