using DemoExam.Core.ObservableObjects;
using DemoExam.Core.Services.Order;
using DemoExam.Core.Services.Products;

namespace DemoExam.Core.Services.ViewModelServices.Products;

public class ProductsViewModelService : IProductsViewModelService
{
    private readonly IOrderService _orderService;
    private readonly IProductsService _productsService;

    public ProductsViewModelService(IProductsService productsService, IOrderService orderService)
    {
        _productsService = productsService;
        _orderService = orderService;
    }

    public Task<int> GetProductsCount()
    {
        return _productsService.Count();
    }

    public Task DeleteProduct(ObservableProduct observableProduct)
    {
        return _productsService.DeleteProduct(observableProduct.Product);
    }

    public Task AddProductToOrder(ObservableProduct observableProduct)
    {
        _orderService.AddProductToOrder(observableProduct.ProductArticleNumber);
        
        return Task.CompletedTask;
    }

    public bool CanOpenOrder()
    {
        return _orderService.HasProductsInOrder();
    }

    public async Task<IEnumerable<ObservableProduct>> GetAllProducts()
    {
        var products = await _productsService.GetAll().ConfigureAwait(false);
        return products.Select(x => new ObservableProduct(x));
    }
}