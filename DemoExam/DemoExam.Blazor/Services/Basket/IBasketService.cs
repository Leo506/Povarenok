using DemoExam.Blazor.Shared.Dto.Responses;

namespace DemoExam.Blazor.Services.Basket;

public interface IBasketService
{
    event Action BasketContentChanged;
    
    int TotalProductsCount { get; }
    Task Initialize();
    
    Task AddProduct(Product product);

    Task RemoveProduct(Product product, bool all = false);

    int GetProductAmount(Product product);

    Dictionary<string, int> GetAll();
    Task Clear();
}