using DemoExam.UITests.Pages;

namespace DemoExam.UITests;

public class CatalogTests : TestFixture
{
    
    [Test]
    public void Search_NoProducts_ShowText()
    {
        new CatalogPage(WebDriver)
            .Search(Guid.NewGuid().ToString())
            .EnsureThereIsEmptyText();
    }
}