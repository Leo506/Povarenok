using DemoExam.Core.Contexts;
using DemoExam.Core.Models;
using DemoExam.Core.NotifyObjects;
using DemoExam.Core.Services.Alert;
using DemoExam.Core.Services.ProductEditService;
using DemoExam.Core.Services.UserRole;
using DemoExam.Core.ViewModels;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace DemoExam.Core.Services.Products;

public class ProductsService : IProductsService
{
    private readonly TradeContext _tradeContext;
    private readonly IUserRoleService _userRoleService;
    private readonly IMvxNavigationService _navigationService;
    private readonly IAlert _alert;
    private readonly IProductEditService _editService;

    public ProductsService(TradeContext tradeContext, IUserRoleService userRoleService, IMvxNavigationService navigationService, IAlert alert, IProductEditService editService)
    {
        _tradeContext = tradeContext;
        _userRoleService = userRoleService;
        _navigationService = navigationService;
        _alert = alert;
        _editService = editService;
    }

    public IEnumerable<ProductNotifyObject> GetAll() =>
        _tradeContext.Products.Select(product => new ProductNotifyObject(product));
    
    public IEnumerable<ProductNotifyObject> GetWhere(Func<Product, bool> predicate) =>
        _tradeContext.Products.Where(predicate).Select(product => new ProductNotifyObject(product)).ToList();

    public int Count() => _tradeContext.Products.Count();
    
    public List<ProductOperation> GetAvailableProductsOperationsForUser(User user)
    {
        if (_userRoleService.IsClientOrGuest(user))
            return GetProductOperationsForClient();
        if (_userRoleService.IsManager(user))
            return GetProductOperationsForManager();
        if (_userRoleService.IsAdmin(user))
            return GetProductOperationsForAdmin();

        return new List<ProductOperation>();
    }



    private List<ProductOperation> GetProductOperationsForClient()
    {
        return new List<ProductOperation>()
        {
            new("Add To Order", new MvxCommand<ProductNotifyObject>(product => { return; })) // TODO Add Product to Order
        };
    }
    
    private List<ProductOperation> GetProductOperationsForManager()
    {
        return new List<ProductOperation>()
        {
            new("Add To Order", new MvxCommand<ProductNotifyObject>(product => { return; }))
        };
    }
    
    private List<ProductOperation> GetProductOperationsForAdmin()
    {
        return new List<ProductOperation>()
        {
            new("Add To Order", new MvxCommand<ProductNotifyObject>(product => { return; })), // TODO Add Product to Order
            new("Edit product",
                new MvxCommand<ProductNotifyObject>(product =>
                    _navigationService.Navigate<ProductEditViewModel, ProductNotifyObject>(product))), // TODO navigate to edit page
            new("Remove product", new MvxCommand<ProductNotifyObject>(product =>
            {
                var choice = _alert.ShowChoice("Deleting product", "Do you shure?");
                if (choice is ChoiceResult.Positive)
                    _editService.DeleteProduct(product);
            })), // TODO Remove product
            new("Add new product", new MvxCommand<ProductNotifyObject>(product => { return; })) // TODO navigate to create page
        };
    }
}