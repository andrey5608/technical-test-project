using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestProject.IntegrationTests;

namespace TestProject.UITests.Tests
{
    class BaseTest
    {
        public IWebDriver _webDriver;
        private WebAppFactory _factory;

        [OneTimeSetUp]
        public void SetUpFactoryAndWebdriver()
        {
            _factory = new WebAppFactory();
            _webDriver = new ChromeDriver();
        }

        [TearDown]
        public void CleanUpCookies()
        {
            _webDriver.Manage().Cookies.DeleteAllCookies();
        }

        [OneTimeTearDown]
        public void CloseDriverAndFactory()
        {
            _webDriver.Close();
            _factory.Dispose();
        }
    }
}
