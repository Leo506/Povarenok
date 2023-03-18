using DemoExam.Core.Models;
using DemoExam.Core.Repositories;

namespace DemoExam.Core.Services.ProductEditService;

public class ProductEditService : IProductEditService
{
    private readonly IProductRepository _repository;

    public ProductEditService(IProductRepository repository)
    {
        _repository = repository;
    }

    public Task SaveProduct(Product product)
    {
        product.Validate();
        return _repository.UpdateAsync(product);
    }
}