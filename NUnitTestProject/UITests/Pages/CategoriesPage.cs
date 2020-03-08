using TestProject.UITests.Elements;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.UITests.Pages
{
    class CategoriesPage : MainPage
    {
        public CategoriesPage(IWebDriver webDriver) : base("Categories page", By.TagName("app-fetch-category"), webDriver)
        {
            _webDriver = webDriver;
        }

        public BaseElement GetCreateNewCategoryButton()
        {
            return new BaseElement("Create new category", By.XPath("//a[@href='/add-category']"), _webDriver);
        }

        public BaseElement GetCategoryRowByName(string Name)
        {
            return new BaseElement("Categories table", 
                By.XPath(string.Format("//app-fetch-category//table//tr[./td[contains(.,'{0}')]]", Name)), _webDriver);
        }

        public BaseElement GetEditButtonByCategoryName(string Name)
        {
            return new BaseElement("Edit category button",
                By.XPath(string.Format("//app-fetch-category//table//tr[./td[contains(.,'{0}')]]//a[contains(@href,'category/edit')]", Name)), _webDriver);
        }

        public BaseElement GetDeleteButtonByCategoryName(string Name)
        {
            return new BaseElement("Delete category button",
                By.XPath(string.Format("//app-fetch-category//table//tr[./td[contains(.,'{0}')]]//a[contains(text(),'Delete')]", Name)), _webDriver);
        }
    }
}
