using DatabaseFiller;
using DemoExam.Domain.Models;
using DemoExam.Infrastructure;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");


var context = new TradeContext();

// Roles
if (context.Roles.Any() is false)
{
    foreach (var item in File.ReadAllLines("Data/Roles.csv"))
        await context.Roles.AddAsync(new Role { Name = item });
    await context.SaveChangesAsync();
}

// Users
if (context.Users.Any() is false)
    await ImportData("Data/User.csv", ';', async data =>
    {
        var tmp = context.Roles.Any();
        var role = await context.Roles.FirstOrDefaultAsync(x => x.Name == data[4]);
        await context.Users.AddAsync(new User
        {
            Surname = data[0],
            Name = data[1].Split()[0],
            Patronymic = data[1].Split()[1],
            Login = data[2],
            Password = data[3],
            RoleId = role.Id
        });
    });

// Pickup points
if (context.PickupPoints.Any() is false)
    await ImportData("Data/PickupPointsImport.csv", ';', async data =>
    {
        await context.PickupPoints.AddAsync(new PickupPoint
        {
            PostIndex = data[0],
            City = data[1],
            Street = data[2]
        });
    });


// Order
if (context.Orders.Any() is false)
    await ImportData("Data/OrderImport.csv", ',', async data =>
    {
        await context.Orders.AddAsync(new Order
        {
            OrderDate = DateTime.Parse(data[1]),
            DeliveryDate = DateTime.Parse(data[2]),
            PickupPointId = int.Parse(data[3]),
            UserId = 1,
            GetCode = int.Parse(data[5]),
            Status = data[6]
        });
    });

// Product
if (context.Products.Any() is false)
    await ImportData("Data/Product.csv", ';', async data =>
    {
        var manufacturer = await context.Manufacturers.FirstOrDefaultAsync(x => x.Name == data[4]);
        if (manufacturer is null)
        {
            manufacturer = (await context.Manufacturers.AddAsync(new Manufacturer()
            {
                Name = data[4]
            })).Entity;
            await context.SaveChangesAsync();
        }

        var supplier = await context.Suppliers.FirstOrDefaultAsync(x => x.Name == data[5]);
        if (supplier is null)
        {
            supplier = (await context.Suppliers.AddAsync(new Supplier()
            {
                Name = data[5]
            })).Entity;
            await context.SaveChangesAsync();
        }
        
        await context.Products.AddAsync(new Product
        {
            ArticleNumber = data[0],
            Name = data[1],
            Price = int.Parse(data[2]),
            ManufacturerId = manufacturer.Id,
            SupplierId = supplier.Id,
            Category = data[6].ToUpper(),
            Discount = byte.Parse(data[7]),
            QuantityInStock = int.Parse(data[8]),
            Description = data[9],
            Photo = ReadToByteArray(data[10])
        });
    });

// Order list
if (context.OrderItems.Any() is false)
    await ImportData("Data/OrderList.csv", ',', async data =>
    {
        await context.OrderItems.AddAsync(new OrderItem()
        {
            OrderId = int.Parse(data[0]),
            ProductId = data[1],
            Amount = int.Parse(data[2])
            /*Order = context.Orders.First(x => x.OrderId == int.Parse(data[0])),
            Product = context.Products.First(x => x.ProductArticleNumber == data[1])*/
        });
    });


async Task ImportData(string path, char delimeter, Func<string[], Task> actionForAdd)
{
    foreach (var item in await File.ReadAllLinesAsync(path)) await actionForAdd(item.Split(delimeter));
    await context.SaveChangesAsync();
}

byte[]? ReadToByteArray(string name)
{
    try
    {
        return FileToByteArrayConverter.Convert($"Pictures/{name}");
    }
    catch (Exception e)
    {
        return null;
    }
}