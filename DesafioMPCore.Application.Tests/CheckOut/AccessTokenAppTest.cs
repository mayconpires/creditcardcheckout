using DesafioMPCore.Application.CheckOut;
using DesafioMPCore.Domain.Interface.Application;
using DesafioMPCore.Domain.Shared;
using DesafioMPCore.Infra.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace DesafioMPCore.Application.Tests.CheckOut
{
    [TestClass]
    public class AccessTokenAppTest
    {
        IAccessTokenApp _accessTokenService;

        [TestMethod]
        public void Should_AuthenticateToCreateUserAccessToken_WithCredentials_Success()
        {
            //Arrange
            var user = new User { UserName = "teste", Password = "123456"};

            Mock<IHttpHandler> _httpHandlerMock = new Mock<IHttpHandler>();
            var tokenJson = BaseTest.UtilTest.FakeTokenJson(accessToken: "1", customerKey: "2", userId: "3");
            var fakeHttpResponseMessage = BaseTest.UtilTest.FakeHttpResponseMessage(tokenJson, 200);

            _httpHandlerMock.Setup(h => h.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>())).ReturnsAsync(fakeHttpResponseMessage);
            _accessTokenService = new AccessTokenApp(_httpHandlerMock.Object, "");

            //Act
            var token = _accessTokenService.AuthenticateToCreateUserAccessToken(user).Result;

            //Check
            _httpHandlerMock.VerifyAll();
            Assert.AreEqual("1", token.AccessToken);
            Assert.AreEqual("2", token.CustomerKey);
            Assert.AreEqual("3", token.UserId);
        }

        [TestMethod]
        public void Should_AuthenticateToCreateUserAccessToken_WithCredentials_NullReturn_Success()
        {
            //Arrange
            var user = new User { UserName = "teste", Password = "123456" };

            Mock<IHttpHandler> _httpHandlerMock = new Mock<IHttpHandler>();
            var tokenJson = "";
            var fakeHttpResponseMessage = BaseTest.UtilTest.FakeHttpResponseMessage(tokenJson, 200);

            _httpHandlerMock.Setup(h => h.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>())).ReturnsAsync(fakeHttpResponseMessage);
            _accessTokenService = new AccessTokenApp(_httpHandlerMock.Object, "");

            //Act
            var token = _accessTokenService.AuthenticateToCreateUserAccessToken(user).Result;

            //Check
            _httpHandlerMock.VerifyAll();
            Assert.IsNull(token);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Should_AuthenticateToCreateUserAccessToken_WithCredentials_ThrowsException_Fail()
        {
            //Arrange
            var user = new User { UserName = "teste", Password = "123456" };

            Mock<IHttpHandler> _httpHandlerMock = new Mock<IHttpHandler>();
            var tokenJson = "";
            var fakeHttpResponseMessage = BaseTest.UtilTest.FakeHttpResponseMessage(tokenJson, 500);

            _httpHandlerMock.Setup(h => h.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>())).ReturnsAsync(fakeHttpResponseMessage);
            _accessTokenService = new AccessTokenApp(_httpHandlerMock.Object, "");

            //Act
            var token = _accessTokenService.AuthenticateToCreateUserAccessToken(user).Result;
        }
    }
}
