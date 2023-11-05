using DemoExam.Blazor.Shared;

namespace DemoExam.Blazor.Services.Basket;

public interface IBasketService
{
    event Action BasketContentChanged;
    
    int TotalProductsCount { get; }
    
    void AddProduct(ProductDto product);

    void RemoveProduct(ProductDto product, bool all = false);

    int GetProductAmount(ProductDto productDto);

    Dictionary<ProductDto, int> GetAll();
}