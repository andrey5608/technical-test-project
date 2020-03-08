using System;
using TestProject.UITests.Elements;
using OpenQA.Selenium;

namespace TestProject.UITests.Pages
{
    class BasePage
    {
        protected string _name;
        protected IWebDriver _webDriver;
        protected By _locator;
        public const string BaseUrl = "http://localhost:59509";

        protected BasePage(string name, By locator)
        {
            _name = name;
            _locator = locator;
        }

        protected BasePage(string name, By locator, IWebDriver driver)
        {
            _webDriver = driver;
            _name = name;
            _locator = locator;            
        }

        public void WaitForIt()
        {
            var basePage = new BaseElement(_name, _locator, _webDriver);
            basePage.WaitForElementToBePresent().AssertExists();
        }

    }
}
