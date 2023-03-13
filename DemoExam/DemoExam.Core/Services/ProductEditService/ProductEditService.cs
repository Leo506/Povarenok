using DemoExam.Core.Contexts;
using DemoExam.Core.ObservableObjects;

namespace DemoExam.Core.Services.ProductEditService;

public class ProductEditService : IProductEditService
{
    private readonly TradeContext _tradeContext;

    public ProductEditService(TradeContext tradeContext)
    {
        _tradeContext = tradeContext;
    }

    public async Task SaveProduct(ObservableProduct observableProduct)
    {
        _tradeContext.Products.Update(observableProduct.Product);
        await _tradeContext.SaveChangesAsync();
    }
}