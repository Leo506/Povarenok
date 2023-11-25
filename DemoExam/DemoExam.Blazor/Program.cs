using Arbus.Network;
using DemoExam.Blazor;
using DemoExam.Blazor.Network;
using DemoExam.Blazor.Services.AccessToken;
using DemoExam.Blazor.Services.Auth;
using DemoExam.Blazor.Services.Basket;
using DemoExam.Blazor.Services.LocalStorage;
using DemoExam.Blazor.Services.PickupPoints;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NativeHttpClient = Arbus.Network.NativeHttpClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient())
    .AddSingleton<IBasketService, BasketService>()
    .AddSingleton<ILocalStorageService, LocalStorageService>()
    .AddScoped<HttpClientContext, PovarenokApiHttpClientContext>()
    .AddScoped<INativeHttpClient, NativeHttpClient>()
    .AddScoped<INetworkManager, NetworkManager>()
    .AddScoped<IAuthService, AuthService>()
    .AddScoped<AuthenticationStateProvider, TokenAuthProvider>()
    .AddScoped<IAccessTokenService, AccessTokenService>()
    .AddScoped<IPickupPointsService, PickupPointsService>()
    .AddAuthorizationCore();
await builder.Build().RunAsync();