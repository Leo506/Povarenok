using DemoExam.Core.Contexts;
using DemoExam.Core.Models;

namespace DemoExam.Core.Services.ProductEditService;

public class ProductEditService : IProductEditService
{
    private readonly TradeContext _tradeContext;

    public ProductEditService(TradeContext tradeContext)
    {
        _tradeContext = tradeContext;
    }

    public async Task SaveProduct(Product product)
    {
        _tradeContext.Products.Update(product);
        await _tradeContext.SaveChangesAsync();
    }
}