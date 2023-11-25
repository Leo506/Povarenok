using DemoExam.UITests.Pages;

namespace DemoExam.UITests;

public class MyOrdersTests : TestFixture
{
    [Test]
    public void ShowMyOrders_Success()
    {
        RegisterAndLogin();
        
        new CatalogPage(WebDriver)
            .AddProductToBasket()
            .ClickBasket();
        new BasketPage(WebDriver)
            .ClickCreateOrder();
        new CatalogPage(WebDriver)
            .ClickUserMenu()
            .ClickMyOrders();
        new MyOrdersPage(WebDriver)
            .EnsureThereIsOrders()
            .ClickOrder()
            .EnsureOrderInfoIsShown();
    }
    
}