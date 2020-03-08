using TestProject.UITests.Pages;
using NUnit.Framework;
using TestProject.UITests.Utils;
using System;

namespace TestProject.UITests.Tests
{
    class CategoryUITests : BaseTest
    {
        [Test]
        public void AnAttemptToCreateCatgeoryWithEmptyName()
        {
            // Arrange
            new LoginHelper(_webDriver).Login();

            // Act
            new MainPage(_webDriver).GetCategoriesButton().Click();
            new CategoriesPage(_webDriver).GetCreateNewCategoryButton().Click();

            var createCategoryPage = new CategoryCreatingPage(_webDriver);
            createCategoryPage.GetSubmitButton().Click();
            // Assert
            createCategoryPage.GetRequiredFieldMissingMessage().WaitForElementToBePresent().AssertExists();
        }

        [Test]
        public void VerifyCreationAndDeletionOfCategory()
        {
            string categoryName = $"Cat number {new Random().Next()}";
            new LoginHelper(_webDriver).Login();

            new MainPage(_webDriver).GetCategoriesButton().Click();
            var categoriesPage = new CategoriesPage(_webDriver);
            categoriesPage.GetCreateNewCategoryButton().Click();

            var createCategoryPage = new CategoryCreatingPage(_webDriver);
            createCategoryPage.GetCategotyNameTextField().SetValue(categoryName);
            createCategoryPage.GetSubmitButton().Click();
            categoriesPage.GetCategoryRowByName(categoryName).WaitForElementToBePresent().AssertExists();

            categoriesPage.GetDeleteButtonByCategoryName(categoryName).Click();
            _webDriver.SwitchTo().Alert().Accept();

            categoriesPage.GetCategoryRowByName(categoryName).WaitForElementInvisibility();
        }
    }
}
