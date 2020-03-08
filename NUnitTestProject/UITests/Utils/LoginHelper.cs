using TestProject.UITests.Pages;
using OpenQA.Selenium;

namespace TestProject.UITests.Utils
{
    class LoginHelper
    {
        private IWebDriver _webDriver;
        public LoginHelper(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void Login(string token = "bccf905c-6592-40f2-8db1-c976791fa40a")
        {
            _webDriver.Navigate().GoToUrl(BasePage.BaseUrl);
            var mainPage = new MainPage(_webDriver);
            mainPage.GetLoginButton().Click();
            var loginPage = new LoginPage(_webDriver);
            loginPage.GetTokenTextField().SetValue(token);
            loginPage.GetLoginSubmitButton().Click();            
        }
    }
}
