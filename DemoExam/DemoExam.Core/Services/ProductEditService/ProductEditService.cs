using DemoExam.Core.ObservableObjects;
using DemoExam.Core.Repositories;

namespace DemoExam.Core.Services.ProductEditService;

public class ProductEditService : IProductEditService
{
    private readonly IProductRepository _repository;

    public ProductEditService(IProductRepository repository)
    {
        _repository = repository;
    }

    public Task SaveProduct(ObservableProduct observableProduct) => 
        _repository.UpdateAsync(observableProduct.Product);
}