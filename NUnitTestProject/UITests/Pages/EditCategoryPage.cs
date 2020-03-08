using TestProject.UITests.Elements;
using OpenQA.Selenium;

namespace TestProject.UITests.Pages
{
    class EditCategoryPage : MainPage
    {
        public EditCategoryPage(IWebDriver webDriver) : base("Edit category page", By.XPath("//app-add-category[./h1[text()='Edit']]//input[@ng-reflect-name='categoryName']"), webDriver)
        {
            _webDriver = webDriver;
        }

        public BaseElement GetCategoryNameField()
        {
            return new BaseElement("Category name textfield", By.XPath("//app-add-category[./h1[text()='Edit']]//input[@ng-reflect-name='categoryName']"), _webDriver);
        }

        public BaseElement GetSubmitButton()
        {
            return new BaseElement("Submit button", By.XPath("//app-add-category[./h1[text()='Edit']]//button[@type='submit']"), _webDriver);
        }
    }
}
