using DemoExam.Core.Models;
using DemoExam.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Database;

public partial class TradeContext : IProductRepository
{
    public Task<List<Product>> GetAllAsync()
    {
        return Products
            .Include(x => x.Manufacturer)
            .Include(x => x.Supplier)
            .ToListAsync();
    }

    public Task<Product?> GetAsync(string articleNumber)
    {
        return Products
            .Include(x => x.Manufacturer)
            .Include(x => x.Supplier)
            .FirstOrDefaultAsync(x => x.ProductArticleNumber == articleNumber);
    }

    public async Task AddAsync(Product product)
    {
        await SetManufacturerToProduct(product);

        await SetSupplierToProduct(product);

        await Products.AddAsync(product);
        await SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Product product)
    {
        await SetManufacturerToProduct(product);

        await SetSupplierToProduct(product);
        
        Products.Update(product);
        await SaveChangesAsync();
    }

    private async Task SetSupplierToProduct(Product product)
    {
        var supplier = await Suppliers.FirstOrDefaultAsync(x => x.SupplierName == product.SupplierName.Trim());
        if (supplier is null)
            product.Supplier = new Supplier() { SupplierName = product.SupplierName };
        else
            product.SupplierId = supplier.Id;
    }

    private async Task SetManufacturerToProduct(Product product)
    {
        var manufacturer =
            await Manufacturers.FirstOrDefaultAsync(x => x.ManufacturerName == product.ManufacturerName.Trim());
        if (manufacturer is null)
            product.Manufacturer = new Manufacturer() { ManufacturerName = product.ManufacturerName };
        else
            product.ManufacturerId = manufacturer.Id;
    }

    public async Task DeleteAsync(Product product)
    {
        Products.Remove(product);
        await SaveChangesAsync();
    }

    public Task<int> Count()
    {
        return Products.CountAsync();
    }

    public Task<bool> Contains(string articleNumber) => 
        Products.AnyAsync(x => x.ProductArticleNumber == articleNumber);
}