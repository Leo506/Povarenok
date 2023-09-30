using DemoExam.Blazor.Shared;

namespace DemoExam.Blazor.Services.Basket;

public class BasketService : IBasketService
{
    private readonly Dictionary<string, int> _products = new();

    public event Action? BasketContentChanged;
    public int TotalProductsCount => _products.Sum(x => x.Value);
    
    public void AddProduct(ProductDto product)
    {
        if (_products.ContainsKey(product.ProductArticleNumber))
            _products[product.ProductArticleNumber]++;
        else
            _products[product.ProductArticleNumber] = 1;
        BasketContentChanged?.Invoke();
    }

    public void RemoveProduct(ProductDto product)
    {
        _products[product.ProductArticleNumber]--;
        if (_products[product.ProductArticleNumber] <= 0)
            _products.Remove(product.ProductArticleNumber);
        BasketContentChanged?.Invoke();
    }

    public int GetProductAmount(ProductDto productDto)
    {
        return _products.TryGetValue(productDto.ProductArticleNumber, out var amount) 
            ? amount 
            : 0;
    }
}