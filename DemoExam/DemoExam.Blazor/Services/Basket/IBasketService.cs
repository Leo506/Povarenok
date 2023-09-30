using DemoExam.Blazor.Shared;

namespace DemoExam.Blazor.Services.Basket;

public interface IBasketService
{
    event Action BasketContentChanged;
    
    int TotalProductsCount { get; }
    
    void AddProduct(ProductDto product);

    void RemoveProduct(ProductDto product);

    int GetProductAmount(ProductDto productDto);
}