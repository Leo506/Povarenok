using OpenQA.Selenium;

namespace DemoExam.UITests.Pages;

public class AlertPage
{
    private readonly IWebDriver _webDriver;

    public AlertPage(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    public void Confirm()
    {
        _webDriver.SwitchTo().Alert().Accept();
    }
}