using TestProject.UITests.Elements;
using OpenQA.Selenium;

namespace TestProject.UITests.Pages
{
    class CategoryCreatingPage : MainPage
    {
        public CategoryCreatingPage(IWebDriver webDriver) : base("Create category page", By.XPath("//app-add-category[./h1[text()='Create']]//input[@ng-reflect-name='categoryName']"), webDriver)
        {
            _webDriver = webDriver;
        }

        public BaseElement GetCategotyNameTextField() => new BaseElement("Category name textfield", By.XPath("//app-add-category[./h1[text()='Create']]//input[@ng-reflect-name='categoryName']"), _webDriver);
        
        public BaseElement GetSubmitButton() => new BaseElement("Submit button", By.XPath("//app-add-category[./h1[text()='Create']]//button[@type='submit']"), _webDriver);
        
        public BaseElement GetRequiredFieldMissingMessage() => new BaseElement("Name is required message", By.XPath("//span[@class='text-danger' and contains(text(),'Name is required')]"), _webDriver);
    }
}
