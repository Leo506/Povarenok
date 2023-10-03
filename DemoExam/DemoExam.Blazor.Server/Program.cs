using DemoExam.Blazor.Server.Mapping;
using DemoExam.Database;
using DemoExam.Domain.Repositories;
using DemoExam.Domain.Services.Order;
using DemoExam.Domain.Services.Products;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder => policyBuilder.AllowAnyOrigin());
});

builder.Services
    .AddTransient<IProductRepository, TradeContext>()
    .AddTransient<IOrderRepository, TradeContext>()
    .AddTransient<IOrderService, OrderService>()
    .AddTransient<IProductsService, ProductsService>()
    .AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();