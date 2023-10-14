using DemoExam.Database;
using DemoExam.Domain.Repositories;
using DemoExam.Domain.Services.Auth;
using DemoExam.Domain.Services.Order;
using DemoExam.Domain.Services.Products;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace DemoExam.Blazor.Server.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.Issuer,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = AuthOptions.SecurityKey,
                    ValidateLifetime = true
                };
            });
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddTransient<IProductRepository, TradeContext>()
            .AddTransient<IOrderRepository, TradeContext>()
            .AddTransient<IUserRepository, TradeContext>();
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IOrderService, OrderService>()
            .AddTransient<IProductsService, ProductsService>()
            .AddScoped<IAuthService, AuthService>();
    }
}