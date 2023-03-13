using System.ComponentModel.DataAnnotations;
using DemoExam.Core.Contexts;
using DemoExam.Core.ObservableObjects;
using Microsoft.IdentityModel.Tokens;

namespace DemoExam.Core.Services.ViewModelServices.AddingProduct;

public class AddingProductViewModelService : IAddingProductViewModelService
{
    private readonly TradeContext _tradeContext;

    public AddingProductViewModelService(TradeContext tradeContext)
    {
        _tradeContext = tradeContext;
    }

    public bool IsValidProduct(ObservableProduct observableProduct)
    {
        if (_tradeContext.Products.Any(x => x.ProductArticleNumber == observableProduct.ProductArticleNumber))
            return false;

        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(observableProduct.Product, new ValidationContext(observableProduct.Product), validationResults, true);

        return validationResults.IsNullOrEmpty();
    }

    public async Task AddProductAsync(ObservableProduct observableProduct)
    {
        await _tradeContext.Products.AddAsync(observableProduct.Product);
        await _tradeContext.SaveChangesAsync();
    }
}