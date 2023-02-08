using DemoExam.Core.NotifyObjects;

namespace DemoExam.Core.Services.ProductEditService;

public interface IProductEditService
{
    Task SaveProduct(ProductNotifyObject product);
}