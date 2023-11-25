using DemoExam.UITests.Extensions;
using FluentAssertions;
using OpenQA.Selenium;

namespace DemoExam.UITests.Pages;

public class BasketPage : PageBase
{
    protected override By Trait => By.XPath("//*[text()='Ваша корзина']");

    public BasketPage(IWebDriver webDriver) : base(webDriver)
    {
    }

    public BasketPage ClickCreateOrder()
    {
        WebDriver.FindByText("Оформить").Click();
        Task.Delay(1_000).GetAwaiter().GetResult();
        return this;
    }

    public BasketPage EnsureThereIsSuccessMessage()
    {
        WebDriver.FindByText("Заказ успешно оформлен").Should().NotBeNull();
        return this;
    }

    public BasketPage EnsureThereIsErrorMessage()
    {
        WebDriver.FindByText("Ошибка").Should().NotBeNull();
        return this;
    }
}