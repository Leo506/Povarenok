namespace DemoExam.Core.Services.Products;

public interface IProductsService
{
    Task<IEnumerable<Product>> GetAll();
    
    Task<int> Count();

    Task DeleteProduct(Product product);

    Task AddProduct(Product product);

    Task UpdateProduct(Product product);
    
    Task AddProductToOrder(Product product);
    
    bool CanOpenOrder();
}