using DemoExam.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Infrastructure;

internal class ProductRepository : IProductRepository
{
    private readonly TradeContext _tradeContext;

    public ProductRepository(TradeContext tradeContext)
    {
        _tradeContext = tradeContext;
    }

    public Task<List<Product>> GetAllAsync()
    {
        return _tradeContext.Products
            .Include(x => x.Manufacturer)
            .Include(x => x.Supplier)
            .ToListAsync();
    }

    public Task<Product?> GetAsync(string articleNumber)
    {
        return _tradeContext.Products
            .Include(x => x.Manufacturer)
            .Include(x => x.Supplier)
            .FirstOrDefaultAsync(x => x.ArticleNumber == articleNumber);
    }

    public async Task AddAsync(Product product)
    {
        await SetManufacturerToProduct(product);

        await SetSupplierToProduct(product);

        await _tradeContext.Products.AddAsync(product);
        await _tradeContext.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Product product)
    {
        await SetManufacturerToProduct(product);

        await SetSupplierToProduct(product);

        var dbProduct = await _tradeContext.Products.FirstOrDefaultAsync(x => x.ArticleNumber == product.ArticleNumber);
        dbProduct?.Update(product);
        _tradeContext.Products.Update(dbProduct!);
        await _tradeContext.SaveChangesAsync();
    }

    private async Task SetSupplierToProduct(Product product)
    {
        var supplier = await _tradeContext.Suppliers.FirstOrDefaultAsync(x => x.Name == product.SupplierName.Trim());
        if (supplier is null)
            product.Supplier = new Supplier() { Name = product.SupplierName };
        else
            product.SupplierId = supplier.Id;
    }

    private async Task SetManufacturerToProduct(Product product)
    {
        var manufacturer =
            await _tradeContext.Manufacturers.FirstOrDefaultAsync(x => x.Name == product.ManufacturerName.Trim());
        if (manufacturer is null)
            product.Manufacturer = new Manufacturer() { Name = product.ManufacturerName };
        else
            product.ManufacturerId = manufacturer.Id;
    }

    public async Task DeleteAsync(Product product)
    {
        _tradeContext.Products.Remove(product);
        await _tradeContext.SaveChangesAsync();
    }

    public Task<int> Count()
    {
        return _tradeContext.Products.CountAsync();
    }

    public Task<bool> Contains(string articleNumber) => 
        _tradeContext.Products.AnyAsync(x => x.ArticleNumber == articleNumber);

    public async Task<IEnumerable<Product>> Find(string searchString, string category)
    {
        var products = await GetAllAsync().ConfigureAwait(false);
        return products.Where(x =>
            x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) && x.Category == category);
    }

    public async Task<IEnumerable<Product>> Find(string searchString)
    {
        var products = await GetAllAsync().ConfigureAwait(false);
        return products.Where(x =>
            x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
    }
}