using DemoExam.Core.Contexts;
using DemoExam.Core.Models;
using DemoExam.Core.Services.UserRole;
using Microsoft.EntityFrameworkCore;
using MvvmCross.Commands;

namespace DemoExam.Core.Services.Products;

public class ProductsService : IProductsService
{
    private readonly TradeContext _tradeContext;
    private IUserRoleService _userRoleService;

    public ProductsService(TradeContext tradeContext, IUserRoleService userRoleService)
    {
        _tradeContext = tradeContext;
        _userRoleService = userRoleService;
    }

    public IEnumerable<Product> GetAll() => _tradeContext.Products.ToList();

    public async Task<IEnumerable<Product>> GetAllAsync() =>
        await _tradeContext.Products.ToListAsync().ConfigureAwait(false);

    public IEnumerable<Product> GetWhere(Func<Product, bool> predicate) =>
        _tradeContext.Products.Where(predicate).ToList();

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
            new("Add To Order", new MvxCommand<Product>(product => { return; })) // TODO Add Product to Order
        };
    }
    
    private List<ProductOperation> GetProductOperationsForManager()
    {
        return new List<ProductOperation>()
        {
            new("Add To Order", new MvxCommand<Product>(product => { return; }))
        };
    }
    
    private List<ProductOperation> GetProductOperationsForAdmin()
    {
        return new List<ProductOperation>()
        {
            new("Add To Order", new MvxCommand<Product>(product => { return; })), // TODO Add Product to Order
            new("Edit product", new MvxCommand<Product>(product => {return;})),  // TODO navigate to edit page
            new("Remove product", new MvxCommand<Product>(product => {return;})), // TODO Remove product
            new("Add new product", new MvxCommand<Product>(product => {return;})) // TODO navigate to create page
        };
    }
}