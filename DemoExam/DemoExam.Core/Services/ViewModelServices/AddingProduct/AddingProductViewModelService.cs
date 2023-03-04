using DemoExam.Core.Contexts;
using DemoExam.Core.Models;

namespace DemoExam.Core.Services.ViewModelServices.AddingProduct;

public class AddingProductViewModelService : IAddingProductViewModelService
{
    private readonly TradeContext _tradeContext;

    public AddingProductViewModelService(TradeContext tradeContext)
    {
        _tradeContext = tradeContext;
    }

    public bool IsValidProduct(Product product)
    {
        return true;
    }

    public async Task AddProductAsync(Product product)
    {
        await _tradeContext.Products.AddAsync(product);
        await _tradeContext.SaveChangesAsync();
    }
}