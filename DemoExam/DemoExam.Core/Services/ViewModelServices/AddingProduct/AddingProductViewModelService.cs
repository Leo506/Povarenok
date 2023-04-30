using DemoExam.Core.Repositories;

namespace DemoExam.Core.Services.ViewModelServices.AddingProduct;

public class AddingProductViewModelService : IAddingProductViewModelService
{
    private readonly IProductRepository _repository;

    public AddingProductViewModelService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task AddProductAsync(Product product)
    {
        product.Validate();
        await _repository.AddAsync(product).ConfigureAwait(false);
    }
}