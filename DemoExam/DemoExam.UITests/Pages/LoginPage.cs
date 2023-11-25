using FluentAssertions;
using OpenQA.Selenium;

namespace DemoExam.UITests.Pages;

public class LoginPage : PageBase
{
    protected override By Trait => By.XPath("//*[text()='Авторизация']");

    public LoginPage(IWebDriver webDriver) : base(webDriver)
    {
    }

    public LoginPage EnterData(string login, string password)
    {
        WebDriver.FindElement(By.Id("inputLogin")).SendKeys(login);
        WebDriver.FindElement(By.Id("inputPassword")).SendKeys(password);
        return this;
    }

    public LoginPage ClickLoginButton()
    {
        WebDriver.FindElement(By.Id("submitLogin")).Click();
        return this;
    }

    public LoginPage EnsureThereIsErrorMessage()
    {
        WebDriver.FindElement(By.ClassName("alert-danger")).Should().NotBeNull();
        return this;
    }
}
