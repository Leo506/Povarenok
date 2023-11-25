using DemoExam.UITests.Pages;

namespace DemoExam.UITests;

public class OrderTests : TestFixture
{
    [Test]
    public void CreateOrder_Success()
    {
        RegisterAndLogin();

        new CatalogPage(WebDriver)
            .AddProductToBasket(3)
            .ClickBasket();

        new BasketPage(WebDriver)
            .ClickCreateOrder()
            .EnsureThereIsSuccessMessage();
    }

    [Test]
    public void CreateOrder_NotEnoughProductsInStock_ShowError()
    {
        RegisterAndLogin();
        
        new CatalogPage(WebDriver)
            .AddProductToBasket(100)
            .ClickBasket();

        new BasketPage(WebDriver)
            .ClickCreateOrder()
            .EnsureThereIsErrorMessage();
    }
}