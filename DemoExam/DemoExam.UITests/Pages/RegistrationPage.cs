using FluentAssertions;
using OpenQA.Selenium;

namespace DemoExam.UITests.Pages;

public class RegistrationPage : PageBase
{
    protected override By Trait => By.XPath("//*[text()='Регистрация']");

    public RegistrationPage(IWebDriver webDriver) : base(webDriver)
    {
    }

    public RegistrationPage EnterData(string name, string surname, string patronymic, string login, string password)
    {
        WebDriver.FindElement(By.Id("inputName")).SendKeys(name);
        WebDriver.FindElement(By.Id("inputSurname")).SendKeys(surname);
        WebDriver.FindElement(By.Id("inputPatronymic")).SendKeys(patronymic);
        WebDriver.FindElement(By.Id("inputLogin")).SendKeys(login);
        WebDriver.FindElement(By.Id("inputPassword")).SendKeys(password);
        return this;
    }

    public RegistrationPage ClickRegisterButton()
    {
        WebDriver.FindElement(By.Id("submitRegistration")).Click();
        return this;
    }

    public RegistrationPage EnsureThereIsErrorMessage()
    {
        WebDriver.FindElement(By.ClassName("alert-danger")).Should().NotBeNull();
        return this;
    }
}