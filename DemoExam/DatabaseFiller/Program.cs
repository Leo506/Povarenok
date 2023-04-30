using DemoExam.Core.Utils;
using DemoExam.Database;
using DemoExam.Domain.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");


var context = new TradeContext();

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
            OrderData = DateTime.Parse(data[1]),
            OrderDeliveryDate = DateTime.Parse(data[2]),
            OrderPickupPoint = int.Parse(data[3]),
            ClientName = data[4],
            GetCode = int.Parse(data[5]),
            OrderStatus = data[6]
        });
    });

// Product
if (context.Products.Any() is false)
    await ImportData("Data/Product.csv", ';', async data =>
    {
        var manufacturer = await context.Manufacturers.FirstOrDefaultAsync(x => x.ManufacturerName == data[4]);
        if (manufacturer is null)
        {
            manufacturer = (await context.Manufacturers.AddAsync(new Manufacturer()
            {
                ManufacturerName = data[4]
            })).Entity;
            await context.SaveChangesAsync();
        }

        var supplier = await context.Suppliers.FirstOrDefaultAsync(x => x.SupplierName == data[5]);
        if (supplier is null)
        {
            supplier = (await context.Suppliers.AddAsync(new Supplier()
            {
                SupplierName = data[5]
            })).Entity;
            await context.SaveChangesAsync();
        }
        
        await context.Products.AddAsync(new Product
        {
            ProductArticleNumber = data[0],
            ProductName = data[1],
            ProductCost = int.Parse(data[2]),
            MaxDiscount = byte.Parse(data[3]),
            ManufacturerId = manufacturer.Id,
            SupplierId = supplier.Id,
            ProductCategory = data[6],
            CurrentDiscount = byte.Parse(data[7]),
            ProductQuantityInStock = int.Parse(data[8]),
            ProductDescription = data[9],
            ProductPhoto = ReadToByteArray(data[10])
        });
    });

// Order list
if (context.OrderLists.Any() is false)
    await ImportData("Data/OrderList.csv", ',', async data =>
    {
        await context.OrderLists.AddAsync(new OrderList
        {
            OrderId = int.Parse(data[0]),
            ProductId = data[1],
            Amount = int.Parse(data[2])
            /*Order = context.Orders.First(x => x.OrderId == int.Parse(data[0])),
            Product = context.Products.First(x => x.ProductArticleNumber == data[1])*/
        });
    });

// Roles
if (context.Roles.Any() is false)
{
    foreach (var item in File.ReadAllLines("Data/Roles.csv"))
        await context.Roles.AddAsync(new Role { RoleName = item });
    await context.SaveChangesAsync();
}

// Users
if (context.Users.Any() is false)
    await ImportData("Data/User.csv", ';', async data =>
    {
        var tmp = context.Roles.Any();
        var role = await context.Roles.FirstOrDefaultAsync(x => x.RoleName == data[4]);
        await context.Users.AddAsync(new User
        {
            UserSurname = data[0],
            UserName = data[1].Split()[0],
            UserPatronymic = data[1].Split()[1],
            UserLogin = data[2],
            UserPassword = data[3],
            UserRole = role.RoleId
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