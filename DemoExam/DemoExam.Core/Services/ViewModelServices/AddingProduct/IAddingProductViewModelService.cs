using DemoExam.Core.Models;

namespace DemoExam.Core.Services.ViewModelServices.AddingProduct;

public interface IAddingProductViewModelService
{
    bool IsValidProduct(Product product);
    Task AddProductAsync(Product product);
}