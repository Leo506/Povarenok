using DemoExam.Domain.Models;

namespace DemoExam.Domain.Services.Products;

public interface IProductsService
{
    Task<IEnumerable<Product>> GetAll();

    Task DeleteProduct(string article);

    Task AddProduct(Product product);

    Task UpdateProduct(string productArticle, Action<Product> productEditAction);

    Task<bool> Exists(string article);

    Task<Product> GetByArticleNumber(string article);
    Task<IEnumerable<Product>> FindProduct(string searchString, string category);
}