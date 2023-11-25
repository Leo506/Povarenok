using OpenQA.Selenium;

namespace DemoExam.UITests.Extensions;

public static class WebDriverExtensions
{
    public static IWebElement FindByText(this IWebDriver webDriver, string text)
    {
        return webDriver.FindElement(By.XPath($"//*[text()[contains(., '{text}')]]"));
    }
}