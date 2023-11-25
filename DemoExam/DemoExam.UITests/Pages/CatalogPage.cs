using DemoExam.UITests.Extensions;
using FluentAssertions;
using OpenQA.Selenium;

namespace DemoExam.UITests.Pages;

public class CatalogPage : PageBase
{
    protected override By Trait => By.XPath("//*[text()='Каталог товаров']");

    public CatalogPage(IWebDriver webDriver) : base(webDriver)
    {
    }

    public CatalogPage Search(string query)
    {
        var searchString = WebDriver.FindElement(By.Id("searchString"));
        searchString.SendKeys(query);
        searchString.SendKeys(Keys.Enter);
        return this;
    }

    public CatalogPage EnsureThereIsEmptyText()
    {
        WebDriver.FindByText("Нет товаров").Should().NotBeNull();
        return this;
    }

    public CatalogPage AddProductToBasket(int amount = 1)
    {
        WebDriver.FindElement(By.ClassName("card"));
        WebDriver.FindByText("В корзину").Click();
        for (int i = 0; i < amount - 1; i++)
        {
            WebDriver.FindElement(By.ClassName("bi-plus")).Click();
        }

        return this;
    }
}