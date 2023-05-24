using DemoExam.Core.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace DemoExam.Core.Services.Products;

public class ProductsService : IProductsService
{
    private readonly IProductRepository _repository;

    public ProductsService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        var products = await _repository.GetAllAsync();
        return products.IsNullOrEmpty()
            ? Enumerable.Empty<Product>()
            : products;
    }

    public Task<int> Count() => _repository.Count();

    public Task DeleteProduct(Product product) => _repository.DeleteAsync(product);

    public Task AddProduct(Product product)
    {
        product.Validate();
        return _repository.AddAsync(product);
    }

    public Task UpdateProduct(Product product) => _repository.UpdateAsync(product);
}