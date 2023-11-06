using DemoExam.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DemoExam.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection collection)
    {
        return collection
            .AddDbContext<TradeContext>()
            .AddScoped<ICategoriesRepository, CategoriesRepository>()
            .AddScoped<IManufacturerRepository, ManufacturerRepository>()
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IOrdersRepository, OrdersRepository>()
            .AddScoped<IPickupPointRepository, PickupPointRepository>()
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<ISupplierRepository, SupplierRepository>()
            .AddScoped<IUserRepository, UserRepository>();
    }
}