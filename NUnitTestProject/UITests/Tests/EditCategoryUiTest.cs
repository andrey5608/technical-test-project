//using TestProject.UITests.Pages;
//using linnworksTest.Utils;
//using NUnit.Framework;
//using System;

//namespace TestProject.UITests.Tests
//{
//    class EditCategoryUiTest : BaseTest
//    {

//        [Test]
//        public void VerifyCategoryEditing()
//        {
//            string categoryName = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
//            LoginHelper.Login();

//            new MainPage().GetCategoriesButton().Click();
//            CategoriesPage categoriesPage = new CategoriesPage();
//            categoriesPage.GetCreateNewCategoryButton().Click();

//            CreateCategoryPage createCategoryPage = new CreateCategoryPage();
//            createCategoryPage.GetCategotyNameTextField().SetValue(categoryName);
//            createCategoryPage.GetSubmitButton().Click();
//            categoriesPage = new CategoriesPage();
//            categoriesPage.GetCategoryRowByName(categoryName).WaitForElementToBePresent().AssertExists();

//            categoriesPage.GetEditButtonByCategoryName(categoryName).Click();

//            string newCategoryName = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
//            EditCategoryPage editCategoryPage = new EditCategoryPage();
//            editCategoryPage.GetCategotyNameTextField().SetValue(newCategoryName);
//            editCategoryPage.GetSubmitButton().Click();

//            categoriesPage = new CategoriesPage();
//            categoriesPage.GetCategoryRowByName(newCategoryName).WaitForElementToBePresent().AssertExists();

//        }
//    }
//}
