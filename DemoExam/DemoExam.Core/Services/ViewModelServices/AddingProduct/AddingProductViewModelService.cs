using DemoExam.Core.Contexts;
using DemoExam.Core.Models;
using DemoExam.Core.NotifyObjects;

namespace DemoExam.Core.Services.ViewModelServices.AddingProduct;

public class AddingProductViewModelService : IAddingProductViewModelService
{
    private readonly TradeContext _tradeContext;

    public AddingProductViewModelService(TradeContext tradeContext)
    {
        _tradeContext = tradeContext;
    }

    public bool IsValidProduct(ProductNotifyObject product)
    {
        return true;
    }

    public async Task AddProductAsync(ProductNotifyObject product)
    {
        await _tradeContext.Products.AddAsync(product.Product);
        await _tradeContext.SaveChangesAsync();
    }
}