using DesafioMPCore.Application.CheckOut;
using DesafioMPCore.Application.Tests.BaseTest;
using DesafioMPCore.Domain.CheckOut;
using DesafioMPCore.Domain.Interface.Application;
using DesafioMPCore.Infra.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace DesafioMPCore.Application.Tests.CheckOut
{
    [TestClass]
    public class MerchantAppTest
    {
        IMerchantApp _merchantApp;

        [TestMethod]
        public void Should_SeekMerchants_WithCreditCardTransaction_Success()
        {
            //Arrange
            var userID = "1234";

            Mock<IHttpHandler> _httpHandlerMock = new Mock<IHttpHandler>();

            var fakeMerchant = BaseTest.UtilTest.FakeMerchant(merchantKey: "1", publicMerchantKey: "2", isEnabled: true, merchantStatus: "Approved");
            var fakeMerchantResult = BaseTest.UtilTest.FakeMerchantResponse(items: new List<Merchant> { fakeMerchant });
            var fakeHttpResponseMessage = UtilTest.FakeHttpResponseMessage(UtilTest.SerializeObject(fakeMerchantResult), 200);

            _httpHandlerMock.Setup(h => h.GetAsync(It.IsAny<string>())).ReturnsAsync(fakeHttpResponseMessage);
            _httpHandlerMock.SetupProperty(h => h.HttpClient, new HttpClient());
            _merchantApp = new MerchantApp(_httpHandlerMock.Object, "");

            //Act
            var merchants = _merchantApp.SeekMerchants(userID).Result;

            //Check
            _httpHandlerMock.Verify(a => a.GetAsync(It.IsAny<string>()));
            Assert.AreEqual("1", merchants.FirstOrDefault().MerchantKey);
            Assert.AreEqual("2", merchants.FirstOrDefault().PublicMerchantKey);
            Assert.AreEqual("Approved", merchants.FirstOrDefault().MerchantStatus);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Should_SeekMerchants_WithCreditCardTransaction_HttpErrorInResponse_ThrowsException_Fail()
        {
            //Arrange
            var userID = "1234";

            Mock<IHttpHandler> _httpHandlerMock = new Mock<IHttpHandler>();

            var fakeMerchant = BaseTest.UtilTest.FakeMerchant(merchantKey: "1", publicMerchantKey: "2", isEnabled: true, merchantStatus: "Approved");
            var fakeMerchantResult = BaseTest.UtilTest.FakeMerchantResponse(items: new List<Merchant> { fakeMerchant });
            var fakeHttpResponseMessage = UtilTest.FakeHttpResponseMessage(UtilTest.SerializeObject(fakeMerchantResult), 500);

            _httpHandlerMock.Setup(h => h.GetAsync(It.IsAny<string>())).ReturnsAsync(fakeHttpResponseMessage);
            _httpHandlerMock.SetupProperty(h => h.HttpClient, new HttpClient());
            _merchantApp = new MerchantApp(_httpHandlerMock.Object, "");

            //Act
            var merchants = _merchantApp.SeekMerchants(userID).Result;
        }

    }
}
