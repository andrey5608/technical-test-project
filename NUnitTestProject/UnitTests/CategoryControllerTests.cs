using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TestProject.Controllers;
using TestProject.DataAccess;

namespace TestProject.Tests.Controllers.UnitTests
{
    [TestFixture]
    [Category("CategoryControllerUnitTests")]
    class CategoryControllerTests
    {
        private Mock<IGenericRepository<DataAccess.Category>> _categoryRepository;
        private CategoryController _categoryController;

        public CategoryControllerTests()
        {
            _categoryRepository = new Mock<IGenericRepository<DataAccess.Category>>();
            _categoryController = new CategoryController(_categoryRepository.Object);
        }

        [Test]
        public void CategoryControllerIndexPositiveTest()
        {
            // Arrange
            var listOfCats = new List<DataAccess.Category>
            {
                new DataAccess.Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "test cat 1",
                    Products = new Mock<ICollection<Product>>().Object
                }
            };

            _categoryRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(listOfCats));

            // Act
            var categoriesIndex = _categoryController.Index().Result;

            // Assert
            categoriesIndex.Count().Should().Be(1, "We expect that returned list will have one category.");
            categoriesIndex.First().CategoryId.Should().Be(listOfCats.First().Id, "Category id must be equal to mock cat id.");
            categoriesIndex.First().Stock.Should().Be(0, "None of the products in stock should be in this category.");
        }

        [Test]
        public void CategoryControllerDetailsFoundTest()
        {
            // Arrange
            var expectedCategory = new DataAccess.Category
            {
                Id = Guid.NewGuid(),
                CategoryName = "test cat 200",
                Products = new Mock<ICollection<Product>>().Object
            };

            _categoryRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(expectedCategory));

            // Act
            var categoryRequest = _categoryController.Details(expectedCategory.Id.ToString("D"));
            var categoryModel = categoryRequest.Result as OkObjectResult;

            // Assert
            var catModel = categoryModel.Value as Models.Category;
            catModel.Should().NotBeNull("We expect that returned category will be not null.");

            catModel.CategoryName.Should().Be(expectedCategory.CategoryName, "Category name must be equal to mock cat name.");
            catModel.CategoryId.Should().Be(expectedCategory.Id, "Category id must be equal to mock cat id.");
        }

        [Test]
        public void CategoryControllerDetailsNotFoundIdFailureTest()
        {
            // Arrange
            DataAccess.Category expectedCategory = null;
            _categoryRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(expectedCategory));

            // Act
            var categoryRequest = _categoryController.Details(Guid.NewGuid().ToString("D"));
            var catNotFoundResult = categoryRequest.Result as NotFoundResult;

            // Assert
            catNotFoundResult.Should().NotBeNull("We expect that returned result will not be null.");
            catNotFoundResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound, "Http code should be 404 Not Found.");
        }

        [Test]
        public void CategoryControllerDetailsNonGuidIdFailureTest()
        {
            // Act
            var categoryRequest = _categoryController.Details("D");
            var catNotFoundResult = categoryRequest.Result as BadRequestResult;

            // Assert
            catNotFoundResult.Should().NotBeNull("We expect that returned result will not be null.");
            catNotFoundResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest, "Http code should be 400 Bad request.");
        }
    }
}
