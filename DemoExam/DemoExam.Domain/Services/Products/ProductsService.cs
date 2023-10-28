using DemoExam.Domain.Models;
using DemoExam.Domain.Repositories;
using DemoExam.Domain.Services.Order;

namespace DemoExam.Domain.Services.Products;

public class ProductsService : IProductsService
{
    private readonly IProductRepository _repository;
    private readonly IOrderService _orderService;

    public ProductsService(IProductRepository repository, IOrderService orderService)
    {
        _repository = repository;
        _orderService = orderService;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        var products = await _repository.GetAllAsync();
        return products.Any() ? products : Enumerable.Empty<Product>();
    }

    public Task<int> Count() => _repository.Count();

    public Task DeleteProduct(Product product) => _repository.DeleteAsync(product);

    public Task AddProduct(Product product)
    {
        product.Validate();
        return _repository.AddAsync(product);
    }

    public Task UpdateProduct(Product product)
    {
        product.Validate();
        return _repository.UpdateAsync(product);
    }

    public Task AddProductToOrder(Product product)
    {
        _orderService.AddProductToOrder(product.ArticleNumber);
        
        return Task.CompletedTask;
    }

    public bool CanOpenOrder() => _orderService.HasProductsInOrder();
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