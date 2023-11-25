using DemoExam.UITests.Extensions;
using FluentAssertions;
using OpenQA.Selenium;

namespace DemoExam.UITests.Pages;

public class ProductsPage : PageBase
{
    private readonly By _deleteButton = By.ClassName("bi-trash");
    private readonly By _editButton = By.ClassName("bi-pencil");
    private readonly By _nameInput = By.Id("name_input");
    private readonly By _articleInput = By.Id("article_input");
    private readonly By _descriptionInput = By.Id("description_input");
    private readonly By _categoryInput = By.Id("category_input");
    private readonly By _manufacturerInput = By.Id("manufacturer_input");
    private readonly By _supplierInput = By.Id("supplier_input");
    protected override By Trait => By.XPath("//*[text()='Товары']");
    
    public ProductsPage(IWebDriver webDriver) : base(webDriver)
    {
    }

    public ProductsPage ClickDeleteButton()
    {
        WebDriver.FindElement(_deleteButton).Click();
        return this;
    }

    public ProductsPage ClickEditButton()
    {
        WebDriver.FindElement(_editButton).Click();
        return this;
    }

    public ProductsPage SetName(string name)
    {
        var input = WebDriver.FindElement(_nameInput);
        input.Clear();
        input.SendKeys(name);
        return this;
    }

    public ProductsPage ClickSave()
    {
        Task.Delay(1_000).GetAwaiter().GetResult();
        WebDriver.FindByText("Сохранить").Click();
        Task.Delay(1_000).GetAwaiter().GetResult();
        return this;
    }

    public ProductsPage EnsureThereIdProductWithName(string name)
    {
        WebDriver.FindByText(name).Should().NotBeNull();
        return this;
    }

    public ProductsPage SetArticle(string article)
    {
        var input = WebDriver.FindElement(_articleInput);
        input.Clear();
        input.SendKeys(article);
        return this;
    }

    public ProductsPage SetDescription(string description)
    {
        var input = WebDriver.FindElement(_descriptionInput);
        input.Clear();
        input.SendKeys(description);
        return this;
    }


    public ProductsPage SetCategory(string category)
    {
        var input = WebDriver.FindElement(_categoryInput);
        input.Clear();
        input.SendKeys(category);
        return this;
    }

    public ProductsPage SetManufacturer(string manufacturer)
    {
        var input = WebDriver.FindElement(_manufacturerInput);
        input.Clear();
        input.SendKeys(manufacturer);
        return this;
    }

    public ProductsPage SetSupplier(string supplier)
    {
        var input = WebDriver.FindElement(_supplierInput);
        input.Clear();
        input.SendKeys(supplier);
        return this;
    }

    public ProductsPage ClickAddButton()
    {
        WebDriver.FindByText("Добавить").Click();
        Task.Delay(1_000).GetAwaiter().GetResult();
        return this;
    }
}