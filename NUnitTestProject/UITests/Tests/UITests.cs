using NUnit.Framework;
using TestProject.UITests.Tests;

namespace TestProject.Tests
{
    [TestFixture]
    [Category("UITests")]
    class UITests : BaseTest
    {        
        [Test]
        public void LogInSuccessfully()
        {
            _webDriver.Navigate().GoToUrl("http://localhost:59509");
            System.Threading.Thread.Sleep(100000);
        }
    }
}
