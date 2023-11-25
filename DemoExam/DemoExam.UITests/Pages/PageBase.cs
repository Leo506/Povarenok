using DemoExam.UITests.Extensions;
using FluentAssertions;
using OpenQA.Selenium;

namespace DemoExam.UITests.Pages;

public abstract class PageBase
{
    protected readonly IWebDriver WebDriver;
    private readonly By _registerButton = By.Id("registerButton");
    private readonly By _enterButton = By.Id("enterButton");
    private readonly By _userMenu = By.Id("userMenu");
    private readonly By _basketButton = By.Id("basketButton");
    protected abstract By Trait { get; }
    
    public PageBase(IWebDriver webDriver)
    {
        WebDriver = webDriver;
        WebDriver.FindElement(Trait);
    }
    
    public void ClickOnRegisterButton()
    {
        WebDriver.FindElement(_registerButton).Click();
    }

    public void ClickOnEnterButton()
    {
        WebDriver.FindElement(_enterButton).Click();
    }

    public void EnsureUserMenuIsVisible()
    {
        WebDriver.FindElement(_userMenu).Should().NotBeNull();
    }

    public void ClickBasket()
    {
        WebDriver.FindElement(_basketButton).Click();
        Task.Delay(1_000).GetAwaiter().GetResult();
    }

    public PageBase ClickUserMenu()
    {
        WebDriver.FindElement(_userMenu).Click();
        return this;
    }

    public PageBase ClickMyOrders()
    {
        WebDriver.FindByText("Мои Заказы").Click();
        return this;
    }

    public PageBase ClickAllOrders()
    {
        WebDriver.FindByText("Все заказы").Click();
        return this;
    }

    public PageBase ClickProducts()
    {
        WebDriver.FindByText("Товары").Click();
        return this;
    }
}