using System.ComponentModel.DataAnnotations;
using DemoExam.Core.ObservableObjects;
using DemoExam.Core.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace DemoExam.Core.Services.ViewModelServices.AddingProduct;

public class AddingProductViewModelService : IAddingProductViewModelService
{
    private readonly IProductRepository _repository;

    public AddingProductViewModelService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> IsValidProduct(ObservableProduct observableProduct)
    {
        if (await _repository.Contains(observableProduct.ProductArticleNumber).ConfigureAwait(false))
            return false;

        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(observableProduct.Product, new ValidationContext(observableProduct.Product), validationResults, true);

        return validationResults.IsNullOrEmpty();
    }

    public async Task AddProductAsync(ObservableProduct observableProduct) =>
        await _repository.AddAsync(observableProduct.Product).ConfigureAwait(false);
}