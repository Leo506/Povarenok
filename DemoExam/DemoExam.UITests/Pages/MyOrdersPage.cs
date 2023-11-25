using DemoExam.UITests.Extensions;
using FluentAssertions;
using OpenQA.Selenium;

namespace DemoExam.UITests.Pages;

public class MyOrdersPage : PageBase
{
    private readonly By _order = By.XPath("//*[text()[contains(., 'Заказ №')]]");
    private readonly By _orderInfo = By.ClassName("show");
    
    protected override By Trait => By.XPath("//*[text()='Мои заказы']");
    
    public MyOrdersPage(IWebDriver webDriver) : base(webDriver)
    {
    }

    public MyOrdersPage EnsureThereIsOrders()
    {
        WebDriver.FindByText("Заказ №").Should().NotBeNull();
        return this;
    }

    public MyOrdersPage ClickOrder()
    {
        WebDriver.FindByText("Заказ №").Click();
        Task.Delay(1_000).GetAwaiter().GetResult();
        return this;
    }

    public MyOrdersPage EnsureOrderInfoIsShown()
    {
        WebDriver.FindElement(_orderInfo).Should().NotBeNull();
        return this;
    }
}