using DesafioMPCore.API.Controllers;
using DesafioMPCore.API.Tests.BaseTest;
using DesafioMPCore.Domain.CheckOut;
using DesafioMPCore.Domain.Interface.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DesafioMPCore.API.Models;
using System.Net;

namespace DesafioMPCore.API.Tests.Controllers
{
    [TestClass]
    public class MerchantControllerTest
    {

        [TestMethod]
        public void Should_Get_MerchantController_WithUserId_ReturnMarchant_Success()
        {
            //Arrange
            Mock<IMerchantApp> merchantAppMock = new Mock<IMerchantApp>();
            var userAccessTokenController = new MerchantController(merchantAppMock.Object);
            var idUser = "1";

            var merchant = UtilTest.FakeMerchant(merchantKey: "1", publicMerchantKey: "2", isEnabled: true);
            var merchantsResponse = new List<Domain.CheckOut.Merchant> { merchant };

            merchantAppMock.Setup(a => a.SeekMerchants(It.IsAny<string>())).ReturnsAsync(merchantsResponse);
           
            //Act
            ObjectResult actionResult = (ObjectResult)userAccessTokenController.Get(idUser).Result;
            var merchantsReturn = (List<Domain.CheckOut.Merchant>)actionResult.Value;

            //Assert
            merchantAppMock.Verify(v=>v.SeekMerchants(It.Is<string>(i => i == "1")), Times.Once);
            Assert.AreEqual("1", merchantsReturn.FirstOrDefault().MerchantKey);
            Assert.AreEqual("2", merchantsReturn.FirstOrDefault().PublicMerchantKey);
            Assert.AreEqual(true, merchantsReturn.FirstOrDefault().IsEnabled);
        }

        [TestMethod]
        public void Should_Get_MerchantController_WithoutUserId_BadRequest_Fail()
        {
            //Arrange
            Mock<IMerchantApp> merchantAppMock = new Mock<IMerchantApp>();
            var userAccessTokenController = new MerchantController(merchantAppMock.Object);
            var idUser = "";

            var merchant = UtilTest.FakeMerchant(merchantKey: "1", publicMerchantKey: "2", isEnabled: true);
            var merchantsResponse = new List<Domain.CheckOut.Merchant> { merchant };

            merchantAppMock.Setup(a => a.SeekMerchants(It.Is<string>(i => i == idUser))).ReturnsAsync(merchantsResponse);

            //Act
            BadRequestResult actionResult = (BadRequestResult)userAccessTokenController.Get(idUser).Result;

            //Assert
            merchantAppMock.Verify(v=>v.SeekMerchants(It.IsAny<string>()), Times.Never);
            Assert.AreEqual(400, actionResult.StatusCode);
        }

        [TestMethod]
        public void Should_Get_MerchantController_WithoutMerchantReturn_NoContent_204_Fail()
        {
            //Arrange
            Mock<IMerchantApp> merchantAppMock = new Mock<IMerchantApp>();
            var userAccessTokenController = new MerchantController(merchantAppMock.Object);
            var idUser = "1";
            List<Domain.CheckOut.Merchant> merchantsResponse = null;

            merchantAppMock.Setup(a => a.SeekMerchants(It.Is<string>(i => i == idUser))).ReturnsAsync(merchantsResponse);

            //Act
            NoContentResult actionResult = (NoContentResult)userAccessTokenController.Get(idUser).Result;

            //Assert
            merchantAppMock.Verify(v=>v.SeekMerchants(It.Is<string>(i => i == "1")), Times.Once);
            Assert.AreEqual(204, actionResult.StatusCode);
        }

        [TestMethod]
        public void Should_Get_MerchantController_MerchantReturn_ThrowsException_InternalServerError_500_Fail()
        {
            //Arrange
            Mock<IMerchantApp> merchantAppMock = new Mock<IMerchantApp>();
            var userAccessTokenController = new MerchantController(merchantAppMock.Object);
            var idUser = "1";

            merchantAppMock.Setup(a => a.SeekMerchants(It.Is<string>(i => i == idUser))).Throws(new Exception());

            //Act
            ObjectResult actionResult = (ObjectResult)userAccessTokenController.Get(idUser).Result;
            var errorModel = (Error)actionResult.Value;

            //Assert
            merchantAppMock.Verify(v => v.SeekMerchants(It.Is<string>(i => i == "1")), Times.Once);
            Assert.AreEqual(500, actionResult.StatusCode);
            Assert.AreEqual("Erro no servidor", errorModel.ErrorDescription);
            Assert.AreEqual((HttpStatusCode)500, errorModel.HttpStatusCode);
        }

    }
}
