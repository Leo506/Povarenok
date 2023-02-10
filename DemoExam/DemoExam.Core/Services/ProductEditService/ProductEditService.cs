using DemoExam.Core.Contexts;
using DemoExam.Core.NotifyObjects;

namespace DemoExam.Core.Services.ProductEditService;

public class ProductEditService : IProductEditService
{
    private readonly TradeContext _tradeContext;

    public ProductEditService(TradeContext tradeContext)
    {
        _tradeContext = tradeContext;
    }

    public async Task SaveProduct(ProductNotifyObject product)
    {
        _tradeContext.Products.Update(product.Product);
        await _tradeContext.SaveChangesAsync();
    }

    public void DeleteProduct(ProductNotifyObject product)
    {
        _tradeContext.Products.Remove(product.Product);
        _tradeContext.SaveChanges();
    }
}