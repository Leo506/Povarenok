using DemoExam.Blazor.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DemoExam.Blazor.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogController : ControllerBase
{
    [HttpGet("")]
    public IActionResult GetCatalog()
    {
        Thread.Sleep(2_000);
        
        var products = new List<ProductDto>();
        for (int i = 0; i < 10; i++)
        {
            products.Add(new()
            {
                ProductArticleNumber = i.ToString(),
                ProductName = "Вилка",
                ProductCost = 153,
                ProductCategory = "Вилки",
                ProductPhoto = Convert.ToBase64String(System.IO.File.ReadAllBytes(@"D:\DemoExam\DemoExam\DatabaseFiller\Pictures\B736H6.jpg"))
            });

        }
        
        return Ok(products);
    }
    
    [HttpGet("search")]
    public IActionResult FindProducts([FromQuery] string searchString, [FromQuery] string category)
    {
        Thread.Sleep(2_000);
        
        var products = new List<ProductDto>();
        for (int i = 0; i < 3; i++)
        {
            products.Add(new()
            {
                ProductArticleNumber = i.ToString(),
                ProductName = "Вилка",
                ProductCost = 153,
                ProductPhoto = Convert.ToBase64String(System.IO.File.ReadAllBytes(@"D:\DemoExam\DemoExam\DatabaseFiller\Pictures\B736H6.jpg"))
            });

        }
        
        return Ok(products);
    }
    
    [HttpGet("{article}")]
    public IActionResult FindProducts(string article)
    {
        return Ok(new ProductDto()
        {
            ProductArticleNumber = article,
            ProductName = "Вилка",
            ProductCost = 153,
            ProductPhoto = Convert.ToBase64String(System.IO.File.ReadAllBytes(@"D:\DemoExam\DemoExam\DatabaseFiller\Pictures\B736H6.jpg"))
        });
    }
}