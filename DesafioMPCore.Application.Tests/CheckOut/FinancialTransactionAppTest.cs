using DesafioMPCore.Application.CheckOut;
using DesafioMPCore.Application.Tests.BaseTest;
using DesafioMPCore.Domain.CheckOut;
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
using System.Linq;

namespace DesafioMPCore.Application.Tests.CheckOut
{
    [TestClass]
    public class FinancialAppTest
    {
        IFinancialTransactionApp _financialTransactionApp;

        [TestMethod]
        public void Should_DoSaleWithCreditCardTransaction_WithCreditCardTransaction_Success()
        {
            //Arrange
            var fakeCreditCardTransaction = BaseTest.UtilTest.FakeCreditCardTransaction(12345, UtilTest.FakeCreditCard("Visa", "2111311141115111", 1, 2019, "Meu Cartao Nome", false, "456"));
            var saleByCreditCard = UtilTest.FakeSaleByCreditCard("1", new List<CreditCardTransaction>() { fakeCreditCardTransaction }) ;

            Mock<IHttpHandler> _httpHandlerMock = new Mock<IHttpHandler>();
            var creditCardTransactionResult = UtilTest.FakeCreditCardTransactionResult(acquirerMessage: "Success", success: true, transactionKey: "12345");
            var fakeCreditCardSaleResponse = BaseTest.UtilTest.FakeCreditCardSaleResponse(buyerKey: "12", creditCardTransactionResultCollection: 
                                                                         new List<CreditCardTransactionResult> { creditCardTransactionResult }, internalTime: 0);

            var fakeHttpResponseMessage = BaseTest.UtilTest.FakeHttpResponseMessage(UtilTest.SerializeObject(fakeCreditCardSaleResponse), 200);
            _httpHandlerMock.Setup(h => h.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>())).ReturnsAsync(fakeHttpResponseMessage);
            _httpHandlerMock.SetupProperty(h => h.HttpClient, new HttpClient());

            _financialTransactionApp = new FinancialTransactionApp(_httpHandlerMock.Object, "");

            //Act
            var creditCardSaleResponse = _financialTransactionApp.DoSaleWithCreditCardTransaction(saleByCreditCard).Result;

            //Check
            _httpHandlerMock.Verify(a => a.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()));
            Assert.AreEqual("Success", creditCardSaleResponse.CreditCardTransactionResultCollection.FirstOrDefault().AcquirerMessage);
            Assert.AreEqual(true, creditCardSaleResponse.CreditCardTransactionResultCollection.FirstOrDefault().Success);
            Assert.AreEqual("12345", creditCardSaleResponse.CreditCardTransactionResultCollection.FirstOrDefault().TransactionKey);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Should_DoSaleWithCreditCardTransaction_WithCreditCardTransaction_HttpErrorInResponse_ThrowsException_Fail()
        {
            //Arrange
            var fakeCreditCardTransaction = BaseTest.UtilTest.FakeCreditCardTransaction(12345, UtilTest.FakeCreditCard("Visa", "2111311141115111", 1, 2019, "Meu Cartao Nome", false, "456"));
            var saleByCreditCard = UtilTest.FakeSaleByCreditCard("1", new List<CreditCardTransaction>() { fakeCreditCardTransaction });

            Mock<IHttpHandler> _httpHandlerMock = new Mock<IHttpHandler>();
            var creditCardTransactionResult = UtilTest.FakeCreditCardTransactionResult(acquirerMessage: "Success", success: true, transactionKey: "12345");
            var fakeCreditCardSaleResponse = BaseTest.UtilTest.FakeCreditCardSaleResponse(buyerKey: "12", creditCardTransactionResultCollection:
                                                                         new List<CreditCardTransactionResult> { creditCardTransactionResult }, internalTime: 0);

            var fakeHttpResponseMessage = BaseTest.UtilTest.FakeHttpResponseMessage(UtilTest.SerializeObject(fakeCreditCardSaleResponse), 500);
            _httpHandlerMock.Setup(h => h.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>())).ReturnsAsync(fakeHttpResponseMessage);
            _httpHandlerMock.SetupProperty(h => h.HttpClient, new HttpClient());

            _financialTransactionApp = new FinancialTransactionApp(_httpHandlerMock.Object, "");

            //Act
            var creditCardSaleResponse = _financialTransactionApp.DoSaleWithCreditCardTransaction(saleByCreditCard).Result;
        }


    }
}
