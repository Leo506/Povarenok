using DemoExam.UITests.Pages;

namespace DemoExam.UITests;

public class AuthTests : TestFixture
{
    [Test]
    public void Registration_Successful()
    {
        new CatalogPage(WebDriver)
            .ClickOnRegisterButton();

        var randomCredentials = Guid.NewGuid().ToString();
        
        new RegistrationPage(WebDriver)
            .EnterData(randomCredentials, randomCredentials, randomCredentials, randomCredentials, randomCredentials)
            .ClickRegisterButton();
        
        new LoginPage(WebDriver)
            .EnterData(randomCredentials, randomCredentials)
            .ClickLoginButton();

        new CatalogPage(WebDriver)
            .EnsureUserMenuIsVisible();
    }

    [Test]
    public void Registration_DuplicateLogin_ShowErrorMessage()
    {
        new CatalogPage(WebDriver)
            .ClickOnRegisterButton();

        var randomCredentials = Guid.NewGuid().ToString();
        
        new RegistrationPage(WebDriver)
            .EnterData(randomCredentials, randomCredentials, randomCredentials, randomCredentials, randomCredentials)
            .ClickRegisterButton();
        
        new LoginPage(WebDriver)
            .ClickOnRegisterButton();
        
        new RegistrationPage(WebDriver)
            .EnterData(randomCredentials, randomCredentials, randomCredentials, randomCredentials, randomCredentials)
            .ClickRegisterButton()
            .EnsureThereIsErrorMessage();
    }

    [Test]
    public void Login_IncorrectCredentials_ShowErrorMessage()
    {
        new CatalogPage(WebDriver)
            .ClickOnEnterButton();
        var credentials = Guid.NewGuid().ToString();
        
        new LoginPage(WebDriver)
            .EnterData(credentials, credentials)
            .ClickLoginButton()
            .EnsureThereIsErrorMessage();
    }
}