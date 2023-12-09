using DemoExam.UITests.Pages;

namespace DemoExam.UITests;

public class ProductsTests : TestFixture
{
    /*[Test]
    public void DeleteProduct_Success()
    {
        LoginAsAdmin();

        new CatalogPage(WebDriver)
            .ClickUserMenu()
            .ClickProducts();
        new ProductsPage(WebDriver)
            .ClickDeleteButton();
        new AlertPage(WebDriver)
            .Confirm();
    }*/

    [Test]
    public void EditProduct_Success()
    {
        LoginAsAdmin();
        
        new CatalogPage(WebDriver)
            .ClickUserMenu()
            .ClickProducts();
        var newName = Guid.NewGuid().ToString();
        new ProductsPage(WebDriver)
            .ClickEditButton()
            .SetName(newName)
            .ClickSave()
            .EnsureThereIdProductWithName(newName);
    }

    [Test]
    public void CreateNewProduct_Success()
    {
        LoginAsAdmin();
        
        new CatalogPage(WebDriver)
            .ClickUserMenu()
            .ClickProducts();
        
        var name = Guid.NewGuid().ToString();
        var random = new Random();
        var article = "B" + random.Next(0, 10) + "B" + random.Next(0, 10) + "B" + random.Next(0, 10);
        new ProductsPage(WebDriver)
            .ClickAddButton()
            .SetArticle(article)
            .SetName(name)
            .SetDescription(Guid.NewGuid().ToString())
            .SetCategory("ВИЛКИ")
            .SetManufacturer("TEST")
            .SetSupplier("TEST")
            .ClickSave()
            .EnsureThereIdProductWithName(name);
    }
}