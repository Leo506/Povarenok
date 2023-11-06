using DemoExam.Blazor.Shared.Dto.Responses;

namespace DemoExam.Blazor.Services.Basket;

public interface IBasketService
{
    event Action BasketContentChanged;
    
    int TotalProductsCount { get; }
    
    void AddProduct(Product product);

    void RemoveProduct(Product product, bool all = false);

    int GetProductAmount(Product product);

    Dictionary<Product, int> GetAll();
    void Clear();
}