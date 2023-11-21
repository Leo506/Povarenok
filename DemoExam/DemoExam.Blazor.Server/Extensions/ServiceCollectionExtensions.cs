using DemoExam.Domain.Models;
using DemoExam.Domain.Services.Auth;
using DemoExam.Domain.Services.Orders;
using DemoExam.Domain.Services.PickupPoints;
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
        services
            .AddAuthorization(options =>
            {
                options.AddPolicy("AdministratorAndManager", policy =>
                {
                    policy.RequireRole(Role.AdminRoleName, Role.ManagerRoleName);
                });
            });
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IOrdersService, OrdersService>()
            .AddTransient<IProductsService, ProductsService>()
            .AddTransient<IPickupPointService, PickupPointService>()
            .AddScoped<IAuthService, AuthService>();
    }
}