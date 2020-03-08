using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TestProject.UITests.Elements
{
    class BaseElement
    {
        private const int Timeout = 5; // 5 seconds search timeout
        private readonly string _name;
        private readonly By _location;
        private IWebDriver _webDriver;

        public BaseElement(string elementName, By elementLocator, IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _name = elementName;
            _location = elementLocator;
        }

        //public BaseElement(string elementName, By elementLocator)
        //{
        //    _name = elementName;
        //    _location = elementLocator;
        //}

        public IWebElement GetWebElement()
        {
            return _webDriver.FindElement(_location);
        }

        public BaseElement WaitForElementToBePresent()
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(Timeout));
            wait.Until(ExpectedConditions.ElementIsVisible(_location));
            return this;
        }

        public BaseElement WaitForElementInvisibility()
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(Timeout));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(_location));
            return this;
        }

        public BaseElement WaitForElementToBeClickable()
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(Timeout));
            wait.Until(ExpectedConditions.ElementToBeClickable(_location));
            return this;
        }

        public BaseElement AssertExists()
        {
            Assert.IsTrue(GetWebElement().Displayed);
            return this;
        }

        //public BaseElement AssertDisabled()
        //{
        //    Assert.IsFalse(GetWebElement().Enabled);
        //    return this;
        //}

        public void Click()
        {
            WaitForElementToBePresent();
            WaitForElementToBeClickable();
            GetWebElement().Click();
        }

        public void SetValue(string value)
        {
            GetWebElement().Clear();
            GetWebElement().SendKeys(value);
        }
    }
}
