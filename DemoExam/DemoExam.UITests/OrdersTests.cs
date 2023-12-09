using DemoExam.UITests.Pages;

namespace DemoExam.UITests;

public class OrdersTests : TestFixture
{
    [Test]
    public void ShowAllOrders_Success()
    {
        LoginAsAdmin();

        new CatalogPage(WebDriver)
            .ClickUserMenu()
            .ClickAllOrders();

        new OrdersPage(WebDriver)
            .EnsureThereIsOrders();
    }

    /*[Test]
    public void DeleteOrder_Success()
    {
        LoginAsAdmin();
        
        new CatalogPage(WebDriver)
            .AddProductToBasket()
            .ClickBasket();
        new BasketPage(WebDriver)
            .ClickCreateOrder();
        new CatalogPage(WebDriver)
            .ClickUserMenu()
            .ClickAllOrders();
        new OrdersPage(WebDriver)
            .ClickDeleteButton();
        new AlertPage(WebDriver)
            .Confirm();
    }*/

    [Test]
    public void EditOrder_Success()
    {
        LoginAsAdmin();
        
        new CatalogPage(WebDriver)
            .AddProductToBasket()
            .ClickBasket();
        new BasketPage(WebDriver)
            .ClickCreateOrder();
        new CatalogPage(WebDriver)
            .ClickUserMenu()
            .ClickAllOrders();

        new OrdersPage(WebDriver)
            .ClickEditButton()
            .SetOrderStatus("Завершен")
            .ClickSave()
            .EnsureThereIsCompletedOrders();
    }
}