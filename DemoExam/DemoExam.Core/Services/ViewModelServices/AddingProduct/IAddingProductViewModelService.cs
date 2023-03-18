using DemoExam.Core.Models;

namespace DemoExam.Core.Services.ViewModelServices.AddingProduct;

public interface IAddingProductViewModelService
{
    Task AddProductAsync(Product product);
}