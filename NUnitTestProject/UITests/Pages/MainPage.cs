using TestProject.UITests.Elements;
using OpenQA.Selenium;

namespace TestProject.UITests.Pages
{
    class MainPage : BasePage
    {
        public MainPage() : base("Main page", By.XPath("//a[@class='navbar-brand']")){}
        public MainPage(IWebDriver webDriver) : base("Main page", By.XPath("//a[@class='navbar-brand']"), webDriver) { }
        public MainPage(string name, By locator, IWebDriver webDriver) : base(name, locator, webDriver) {}

        public BaseElement GetHomeButton()
        {
            return new BaseElement("Home button", By.XPath("//ul[@class='nav navbar-nav']//a[@href='/']"), _webDriver);
        }

        public BaseElement GetLoginButton()
        {
            return new BaseElement("Login button", By.XPath("//ul[@class='nav navbar-nav']//a[@href='/login']"), _webDriver);
        }

        public BaseElement GetCategoriesButton()
        {
            return new BaseElement("Categories button", By.XPath("//ul[@class='nav navbar-nav']//a[@href='/fetch-category']"), _webDriver);
        }

        public BaseElement GetLogoutButton()
        {
            return new BaseElement("Logout button", By.XPath("//ul[@class='nav navbar-nav']//a[@href='/logout']"), _webDriver);
        }
    }
}
