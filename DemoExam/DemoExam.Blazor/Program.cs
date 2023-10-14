using DemoExam.Blazor;
using DemoExam.Blazor.Services.Auth;
using DemoExam.Blazor.Services.Basket;
using DemoExam.Blazor.Services.LocalStorage;
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
    .AddAuthorizationCore();
await builder.Build().RunAsync();