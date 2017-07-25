using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesafioMPCore.API.Controllers;
using DesafioMPCore.Domain.Interface.Application;
using DesafioMPCore.Application.CheckOut;
using Moq;
using DesafioMPCore.API.Tests.BaseTest;
using DesafioMPCore.Domain.Shared;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DesafioMPCore.API.Models;
using System.Net;

namespace DesafioMPCore.API.Tests
{
    [TestClass]
    public class UserAccessTokenControllerTest
    {

        [TestMethod]
        public void Should_Post_UserAccessTokenController_WithUser_ReturnToken_Success()
        {
            //Arrange
            Mock<IAccessTokenApp> accessTokenAppMock = new Mock<IAccessTokenApp>();
            var userAccessTokenController = new UserAccessTokenController(accessTokenAppMock.Object);
            var user = UtilTest.FakeUser(userName: "Nome 1", password: "1234");

            var tokenResponse = UtilTest.FakeToken(accessToken: "1", customerKey: "2", userId: "3");

            accessTokenAppMock.Setup(a => a.AuthenticateToCreateUserAccessToken(It.IsAny<Domain.Shared.User>())).ReturnsAsync(tokenResponse);

            //Act
            ObjectResult actionResult = (ObjectResult)userAccessTokenController.Post(user).Result;
            var tokenReturn = (Token)actionResult.Value;

            //Assert
            accessTokenAppMock.Verify();
            Assert.AreEqual("1", tokenReturn.AccessToken);
            Assert.AreEqual("2", tokenReturn.CustomerKey);
            Assert.AreEqual("3", tokenReturn.UserId);
        }

        [TestMethod]
        public void Should_Post_UserAccessTokenController_WithoutUser_BadRequest_Fail()
        {
            //Arrange
            Mock<IAccessTokenApp> accessTokenAppMock = new Mock<IAccessTokenApp>();
            var userAccessTokenController = new UserAccessTokenController(accessTokenAppMock.Object);
            Models.User user = null;

            //Act
            BadRequestResult actionResult = (BadRequestResult)userAccessTokenController.Post(user).Result;

            //Assert
            accessTokenAppMock.Verify();
            Assert.AreEqual(400, actionResult.StatusCode);
        }

        [TestMethod]
        public void Should_Post_UserAccessTokenController_WithUser_AuthenticateToCreateUserAccessToken_ThrowsException_Fail()
        {
            //Arrange
            Mock<IAccessTokenApp> accessTokenAppMock = new Mock<IAccessTokenApp>();
            var userAccessTokenController = new UserAccessTokenController(accessTokenAppMock.Object);
            var user = UtilTest.FakeUser(userName: "Nome 1", password: "1234");
            accessTokenAppMock.Setup(a => a.AuthenticateToCreateUserAccessToken(It.IsAny<Domain.Shared.User>())).Throws(new Exception());

            //Act
            ObjectResult actionResult = (ObjectResult)userAccessTokenController.Post(user).Result;
            var errorModel = (Error)actionResult.Value;

            //Assert
            accessTokenAppMock.Verify();
            Assert.AreEqual(500, actionResult.StatusCode);
            Assert.AreEqual("Erro no servidor", errorModel.ErrorDescription);
            Assert.AreEqual((HttpStatusCode)500, errorModel.HttpStatusCode);
        }

        [TestMethod]
        public void Should_Post_UserAccessTokenController_WithUser_AuthenticateToCreateUserAccessToken_Forbid_Fail()
        {
            //Arrange
            Mock<IAccessTokenApp> accessTokenAppMock = new Mock<IAccessTokenApp>();
            var userAccessTokenController = new UserAccessTokenController(accessTokenAppMock.Object);
            var user = UtilTest.FakeUser(userName: "Nome 1", password: "1234");

            Token tokenResponseNull = null;

            accessTokenAppMock.Setup(a => a.AuthenticateToCreateUserAccessToken(It.IsAny<Domain.Shared.User>())).ReturnsAsync(tokenResponseNull);

            //Act
            var actionResultType = (ForbidResult)userAccessTokenController.Post(user).Result;
            ForbidResult actionResult = (ForbidResult)actionResultType;

            //Assert
            accessTokenAppMock.Verify();
            Assert.AreEqual("Login negado.", actionResult.AuthenticationSchemes[0]);
            Assert.IsInstanceOfType(actionResultType, typeof(ForbidResult));
        }
    }
}
