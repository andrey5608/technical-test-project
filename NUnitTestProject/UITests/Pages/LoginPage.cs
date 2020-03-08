using TestProject.UITests.Elements;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.UITests.Pages
{
    class LoginPage : MainPage
    {

        public LoginPage(IWebDriver webDriver) : base("Login page", By.Id("token"), webDriver) { _webDriver = webDriver; }

        public BaseElement GetTokenTextField()
        {
            return new BaseElement("Token text field", By.Id("token"), _webDriver);
        }

        public BaseElement GetLoginSubmitButton()
        {
            return new BaseElement("Login button", By.XPath("//button[@type='submit']"), _webDriver);
        }

        public BaseElement GetInvalidTokenMessage()
        {
            return new BaseElement("Invalid token message", By.XPath("//div[@class='alert alert-danger' and contains(.,'Invalid token.')]"), _webDriver);
        }
    }
}
