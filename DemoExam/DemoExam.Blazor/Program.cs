using DemoExam.Blazor;
using DemoExam.Blazor.Services.AccessToken;
using DemoExam.Blazor.Services.Auth;
using DemoExam.Blazor.Services.Basket;
using DemoExam.Blazor.Services.LocalStorage;
using DemoExam.Blazor.Services.Modals;
using DemoExam.Blazor.Services.Orders;
using DemoExam.Blazor.Services.PickupPoints;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5062") })
    .AddSingleton<IBasketService, BasketService>()
    .AddSingleton<ILocalStorageService, LocalStorageService>()
    .AddScoped<IAuthService, AuthService>()
    .AddScoped<AuthenticationStateProvider, TokenAuthProvider>()
    .AddScoped<IOrdersService, OrdersService>()
    .AddScoped<IAccessTokenService, AccessTokenService>()
    .AddScoped<IPickupPointsService, PickupPointsService>()
    .AddScoped<IModalService, ModalService>()
    .AddAuthorizationCore();
await builder.Build().RunAsync();