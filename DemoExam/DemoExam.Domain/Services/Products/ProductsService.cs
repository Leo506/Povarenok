using DemoExam.Domain.Models;
using DemoExam.Domain.Repositories;

namespace DemoExam.Domain.Services.Products;

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
        return products.Any() ? products : Enumerable.Empty<Product>();
    }
    
    public Task DeleteProduct(string article) => _repository.DeleteAsync(article);

    public Task AddProduct(Product product)
    {
        product.Validate();
        return _repository.AddAsync(product);
    }

    public async Task UpdateProduct(string productArticle, Action<Product> productEditAction)
    {
        var product = await GetByArticleNumber(productArticle);
        productEditAction(product);
        product.Validate();
        await _repository.UpdateAsync(product);
    }

    public Task<bool> Exists(string article) => _repository.Contains(article);

    public async Task<Product> GetByArticleNumber(string article)
    {
        var product = await _repository.GetAsync(article).ConfigureAwait(false);
        return product ?? throw new ArgumentOutOfRangeException();
    }

    public Task<IEnumerable<Product>> FindProduct(string searchString, string category)
    {
        return category == "all" 
            ? _repository.Find(searchString) 
            : _repository.Find(searchString, category);
    }
}