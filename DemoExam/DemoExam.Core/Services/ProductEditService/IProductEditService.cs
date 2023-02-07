using DemoExam.Core.Models;

namespace DemoExam.Core.Services.ProductEditService;

public interface IProductEditService
{
    Task SaveProduct(Product product);
}