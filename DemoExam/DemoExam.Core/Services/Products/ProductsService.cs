using DemoExam.Core.Contexts;
using DemoExam.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Core.Services.Products;

public class ProductsService : IProductsService
{
    private readonly TradeContext _tradeContext;

    public ProductsService(TradeContext tradeContext)
    {
        _tradeContext = tradeContext;
    }

    public IEnumerable<Product> GetAll() => _tradeContext.Products.ToList();

    public async Task<IEnumerable<Product>> GetAllAsync() =>
        await _tradeContext.Products.ToListAsync().ConfigureAwait(false);

    public IEnumerable<Product> GetWhere(Func<Product, bool> predicate) =>
        _tradeContext.Products.Where(predicate).ToList();
}