using DemoExam.Core.ObservableObjects;
using DemoExam.Core.Services.Order;
using DemoExam.Core.Services.Products;
using DemoExam.Core.ViewModels;

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

    public async Task<IEnumerable<ObservableProduct>> SelectProducts(string? search = null, SortOrder sortOrder = SortOrder.None,
        Func<double, bool>? discountSelectorPredicate = null)
    {
        var products = await _productsService.GetAll().ConfigureAwait(false);
        if (string.IsNullOrEmpty(search) is false)
            products = products.Where(p =>
                p.ProductName.ToLowerInvariant().Contains(search.ToLowerInvariant()));

        if (discountSelectorPredicate is not null)
            products = products.Where(x => discountSelectorPredicate(x.CurrentDiscount));

        products = sortOrder switch
        {
            SortOrder.Asc => products.OrderBy(x => x.ProductCost),
            SortOrder.Desc => products.OrderByDescending(x => x.ProductCost),
            _ => products
        };

        return products;
    }

    public Task<int> GetProductsCount()
    {
        return _productsService.Count();
    }

    public Task DeleteProduct(ObservableProduct observableProduct)
    {
        return _productsService.DeleteProduct(observableProduct);
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
}