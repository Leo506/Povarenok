using DemoExam.Domain.Models;

namespace DemoExam.Web.Models;

public class CatalogViewModel
{
    public string SearchString { get; set; } = default!;

    public List<Product> Products { get; set; }

    public CatalogViewModel()
    {
        Products = new List<Product>();
        for (int i = 0; i < 10; i++)
        {
            Products.Add(new()
            {
                ProductArticleNumber = $"{i}",
                ProductName = "Вилка",
                ProductDescription = "gegegegegegegeg",
                ProductCost = 153,
                ProductPhoto = File.ReadAllBytes(@"D:\DemoExam\DemoExam\DatabaseFiller\Pictures\B736H6.jpg")
            });
        }
    }
}