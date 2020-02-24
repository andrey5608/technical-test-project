using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using TestProject.Models;
using System;
using FluentAssertions;

namespace TestProject.IntegrationTests
{
    [TestFixture]
    [Category("CategoryControllerIntegrationTests")]
    public class CategoryControllerIntegrationTests
    {
        private const string ValidTokenAsAString = "bccf905c-6592-40f2-8db1-c976791fa40a";
        private WebAppFactory _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void SetUpFactoryAndHttpClient()
        {
            _factory = new WebAppFactory();
            _client = _factory.CreateClient();
        }

        [SetUp]
        public void PerformLogIn()
        {
            PerformLogin(ValidTokenAsAString).Result.StatusCode.Should().Be(HttpStatusCode.OK);
            // Before each test we need to log in successfully
        }

        private async Task<HttpResponseMessage> PerformLogin(string tokenAsAString)
        {
            var token = new TokenModel
            {
                token = tokenAsAString
            };

            var dataAsString = JsonConvert.SerializeObject(token);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await _client.PostAsync("api/Auth/Login", content);
        }

        private async Task<HttpResponseMessage> PostAsJsonAsync(object data, string url)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await _client.PostAsync(url, content);
        }

        [Test]
        public async Task WhenSomeCategoryIsCreatedThenResultIsOk()
        {
            // Arrange            
            var category = new Category
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = "name11"
            };

            // Act
            var result = await PostAsJsonAsync(category, "/api/Category/Create");

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task WhenWrongModelIsProvidedThenTheResultIsBadRequest()
        {
            // Arrange
            var category = "category name described in wrong format";
            // Act
            var result = await PostAsJsonAsync(category, "/api/Category/Create");
            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task WhenWeDeleteSomeCategoryThenResultIsOk()
        {
            // Arrange
            var category = new Category
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = "name12"
            };
            var result = await PostAsJsonAsync(category, "/api/Category/Create");
            var response = await result.Content.ReadAsStringAsync();
            var responseAsCategory = JsonConvert.DeserializeObject<Category>(response);

            // Act
            var deleteResult = await _client.DeleteAsync($"/api/Category/Delete/{responseAsCategory.CategoryId}");

            // Assert
            Assert.That(deleteResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task WhenWeTryingToDeleteCategoryWithNonGuidIdThenResultIsBadRequest()
        {
            // Arrange
            var invalidId = "itIsAnInvalidId";

            // Act
            var deleteResult = await _client.DeleteAsync($"/api/Category/Delete/{invalidId}");

            // Assert
            Assert.That(deleteResult.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task WhenWeTryToDeleteAlreadyDeletedCategoryThenResultIsInternalServerError()
        {
            // Arrange
            var category = new Category
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = "name13"
            };
            var result = await PostAsJsonAsync(category, "/api/Category/Create");
            var response = await result.Content.ReadAsStringAsync();
            var responseAsCategory = JsonConvert.DeserializeObject<Category>(response);
            var deleteCatResponse = await _client.DeleteAsync($"/api/Category/Delete/{responseAsCategory.CategoryId}");

            // Act
            // We are trying to delete already deleted category from db
            var deleteResult = await _client.DeleteAsync($"/api/Category/Delete/{responseAsCategory.CategoryId}");

            // Assert

            Assert.That(deleteResult.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));

            // here we should expect HttpStatusCode.NotFound if we will check the category presence
            // in the database. So we can modify Delete method in CategoryController.cs
            /*
             *             if (categoryRepository.GetAll().Count(x => x.Id == categoryId).Equals(0))
             *             {
             *                  return new NotFoundResult();
             *             }
             * and after that modification we need to change expected result of this test.
             */
        }

        // TODO: Implement Unauthorized deleting etc

        [OneTimeTearDown]
        public void DisposeFactoryAndHttpClient()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
