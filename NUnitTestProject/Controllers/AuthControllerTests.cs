using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TestProject.Controllers;
using TestProject.DataAccess;
using static TestProject.Controllers.AuthController;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Authentication;

namespace TestProject.Tests.Controllers
{
    class AuthControllerTests
    {
        private Mock<ITokenRepository> _mockRepo;
        private AuthController _controller;
        private readonly Account _account;
        private readonly CategoriesManagementContext _catManContext;
        private readonly TokenRepository _tokenRepo;

        public AuthControllerTests()
        {
            _mockRepo = new Mock<ITokenRepository>();
            _account = new Account
            {
                Token = "00000000-0000-0000-0000-000000000000"
            };
        }

        [Test]
        public void AuthContollerLoginBadRequestTest()
        {
            // Arrange
            _controller = new AuthController(_mockRepo.Object);// here we are not set up http context

            // Act
            var x = _controller.Login(_account).Result;// so we just trying to log in with plain request

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(x);// and we get the bad request result
        }

        [Test]
        public void AuthControllerLoginSuccessTest()
        {
            //Arrange
            // here we set up that any token will validate successfully
            var responseTask = Task.FromResult(true);
            _mockRepo.Setup(r => r.IsValidTokenAsync(It.IsAny<Guid>()))
                .Returns(responseTask);

            // we set up auth servise and controller context
            var services = new Mock<IServiceProvider>();
            services.Setup(provider => provider.GetService(typeof(IAuthenticationService)))
                .Returns(new Mock<IAuthenticationService>().Object);

            _controller = new AuthController(_mockRepo.Object)
            {
                ControllerContext = new ControllerContext(new ActionContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        Session = new Mock<ISession>().Object,
                        RequestServices = services.Object
                    },
                    RouteData = new RouteData(),
                    ActionDescriptor = new ControllerActionDescriptor()
                })
            };

            //Act
            var x = _controller.Login(_account).Result;// and we' re trying to login successfully

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(x);// and check that result here
        }
    }
}
