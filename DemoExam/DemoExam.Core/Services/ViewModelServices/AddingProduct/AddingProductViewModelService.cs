using System.ComponentModel.DataAnnotations;
using DemoExam.Core.Contexts;
using DemoExam.Core.Models;
using DemoExam.Core.NotifyObjects;
using Microsoft.IdentityModel.Tokens;

namespace DemoExam.Core.Services.ViewModelServices.AddingProduct;

public class AddingProductViewModelService : IAddingProductViewModelService
{
    private readonly TradeContext _tradeContext;

    public AddingProductViewModelService(TradeContext tradeContext)
    {
        _tradeContext = tradeContext;
    }

    public bool IsValidProduct(ProductNotifyObject product)
    {
        if (_tradeContext.Products.Any(x => x.ProductArticleNumber == product.ProductArticleNumber))
            return false;

        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(product.Product, new ValidationContext(product.Product), validationResults, true);
        
        return validationResults.IsNullOrEmpty();
    }

    public async Task AddProductAsync(ProductNotifyObject product)
    {
        await _tradeContext.Products.AddAsync(product.Product);
        await _tradeContext.SaveChangesAsync();
    }
}