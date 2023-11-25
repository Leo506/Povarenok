using DemoExam.UITests.Extensions;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DemoExam.UITests.Pages;

public class OrdersPage : PageBase
{
    private readonly By _order = By.TagName("td");
    private readonly By _editButton = By.ClassName("bi-pencil");
    private readonly By _deleteButton = By.ClassName("bi-trash");
    private readonly By _statusInput = By.Id("status_input");
    protected override By Trait => By.XPath("//*[text()='Заказы']");
    
    public OrdersPage(IWebDriver webDriver) : base(webDriver)
    {
    }

    public OrdersPage EnsureThereIsOrders()
    {
        WebDriver.FindElement(_order).Should().NotBeNull();
        return this;
    }

    public OrdersPage ClickDeleteButton()
    {
        WebDriver.FindElement(_deleteButton).Click();
        return this;
    }

    public OrdersPage ClickEditButton()
    {
        WebDriver.FindElement(_editButton).Click();
        Task.Delay(1_000).GetAwaiter().GetResult();
        return this;
    }

    public OrdersPage ClickSave()
    {
        WebDriver.FindByText("Сохранить").Click();
        Task.Delay(1_000).GetAwaiter().GetResult();
        return this;
    }
    

    public OrdersPage SetOrderStatus(string status)
    {
        new SelectElement(WebDriver.FindElement(_statusInput))
            .SelectByText(status);
        return this;
    }

    public OrdersPage EnsureThereIsCompletedOrders()
    {
        WebDriver.FindByText("Завершен").Should().NotBeNull();
        return this;
    }
}