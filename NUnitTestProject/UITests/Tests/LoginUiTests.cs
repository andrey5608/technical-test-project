using TestProject.UITests.Pages;
using NUnit.Framework;
using TestProject.UITests.Utils;

namespace TestProject.UITests.Tests
{   
    class LoginUiTests : BaseTest
    {
        [Test]
        public void VerifyLoginAndLogout()
        {
            new LoginHelper(_webDriver).Login();
            var mainPage = new MainPage(_webDriver);
            mainPage.GetLogoutButton().WaitForElementToBePresent();
            new CategoriesPage(_webDriver).GetLogoutButton().Click();
            
            mainPage.GetLogoutButton().WaitForElementInvisibility();
            mainPage.GetLoginButton().Click();

            new LoginPage(_webDriver).GetTokenTextField().WaitForElementToBePresent().AssertExists();
            
        }

        [Test]
        public void LoginAttemptWithWrongToken()
        {
            var wrongToken = "wrong token";
            new LoginHelper(_webDriver).Login(wrongToken);

            new LoginPage(_webDriver).GetInvalidTokenMessage().WaitForElementToBePresent().AssertExists();
        }
    }
}
