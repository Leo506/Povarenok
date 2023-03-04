using DemoExam.Core.NotifyObjects;

namespace DemoExam.Core.Services.ViewModelServices.AddingProduct;

public interface IAddingProductViewModelService
{
    bool IsValidProduct(ProductNotifyObject product);
    Task AddProductAsync(ProductNotifyObject product);
}