using DemoExam.UITests.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DemoExam.UITests;

public class TestFixture
{
    protected IWebDriver WebDriver;
    
    [SetUp]
    public void Setup()
    {
        WebDriver = new ChromeDriver(new ChromeOptions());
        WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        WebDriver.Navigate().GoToUrl("http://localhost:5029/catalog");
    }

    [TearDown]
    public void TearDown()
    {
        WebDriver.Quit();
    }

    protected void RegisterAndLogin()
    {
        new CatalogPage(WebDriver)
            .ClickOnRegisterButton();

        var credentials = Guid.NewGuid().ToString();

        new RegistrationPage(WebDriver)
            .EnterData(credentials, credentials, credentials, credentials, credentials)
            .ClickRegisterButton();
        new LoginPage(WebDriver)
            .EnterData(credentials, credentials)
            .ClickLoginButton();
    }

    protected void LoginAsAdmin()
    {
        new CatalogPage(WebDriver)
            .ClickOnEnterButton();
        new LoginPage(WebDriver)
            .EnterData("loginDExvq2018", "hX0wJz")
            .ClickLoginButton();
    }
}