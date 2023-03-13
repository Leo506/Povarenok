using DemoExam.Core.ObservableObjects;
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

    public async Task<IEnumerable<ObservableProduct>> GetAll()
    {
        var products = await _repository.GetAllAsync();
        return products.IsNullOrEmpty()
            ? new List<ObservableProduct>()
            : products.Select(product => new ObservableProduct(product));
    }

    public Task<int> Count() => _repository.Count();

    public Task DeleteProduct(ObservableProduct observableProduct) => _repository.DeleteAsync(observableProduct.Product);

    public Task AddProduct(ObservableProduct observableProduct) => _repository.AddAsync(observableProduct.Product);

    public Task UpdateProduct(ObservableProduct observableProduct) => _repository.DeleteAsync(observableProduct.Product);
}