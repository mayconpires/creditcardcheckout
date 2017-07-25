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
    public class TransactionCreditCardControllerTest
    {

        [TestMethod]
        public void Should_Post_TransactionCreditCardController_WithCreditCardTransaction_ReturnCreditCardSaleResponse_Success()
        {
            //Arrange
            Mock<IFinancialTransactionApp> financialTransactionAppMock = new Mock<IFinancialTransactionApp>();
            var transactionCardController = new TransactionCardController(financialTransactionAppMock.Object);
            var creditCard = UtilTest.FakeCreditCard(creditCardBrand: "Visa", creditCardNumber: "1234", expMonth: 1, expYear: 2020, holderName: "Nome no Cartaao",
                                                     isExpiredCreditCard: false, securityCode: "789");

            var creditCardTransaction = UtilTest.FakeCreditCardTransaction(merchantKey: "10", ammount: 7000, creditCardBrand: "Visa", creditCardNumber: "1234", expMonth: 1,
                                                                           expYear: 2020, holderName: "Nome SA", securityCode: "456");

            var creditCardTransactionResultCollection = new List<CreditCardTransactionResult> { UtilTest.FakeCreditCardTransactionResult(acquirerMessage: "Success", success: true, transactionKey: "2") };
            CreditCardSaleResponse creditCardSaleResponse =
                UtilTest.FakeCreditCardSaleResponse(merchantKey: "11", buyerKey: "1", creditCardTransactionResultCollection: creditCardTransactionResultCollection, errorReport: "");

            financialTransactionAppMock.Setup(a => a.DoSaleWithCreditCardTransaction(It.IsAny<SaleByCreditCard>())).ReturnsAsync(creditCardSaleResponse);
           
            //Act
            ObjectResult actionResult = (ObjectResult)transactionCardController.Post(creditCardTransaction).Result;
            var creditCardSaleResponseReturn = (Domain.CheckOut.CreditCardSaleResponse)actionResult.Value;

            //Assert
            financialTransactionAppMock.Verify(v=>v.DoSaleWithCreditCardTransaction(It.IsAny<SaleByCreditCard>()), Times.Once);
            Assert.AreEqual("1", creditCardSaleResponseReturn.BuyerKey);
            Assert.AreEqual("11", creditCardSaleResponseReturn.MerchantKey);
            Assert.AreEqual("Success", creditCardSaleResponseReturn.CreditCardTransactionResultCollection.FirstOrDefault().AcquirerMessage);
            Assert.AreEqual(true, creditCardSaleResponseReturn.CreditCardTransactionResultCollection.FirstOrDefault().Success);
            Assert.AreEqual("2", creditCardSaleResponseReturn.CreditCardTransactionResultCollection.FirstOrDefault().TransactionKey);
        }

        [TestMethod]
        public void Should_Post_TransactionCreditCardController_WithOutCreditCardTransaction_ReturnBadRequest_Fail()
        {
            //Arrange
            Mock<IFinancialTransactionApp> financialTransactionAppMock = new Mock<IFinancialTransactionApp>();
            var transactionCardController = new TransactionCardController(financialTransactionAppMock.Object);
            var creditCard = UtilTest.FakeCreditCard(creditCardBrand: "Visa", creditCardNumber: "1234", expMonth: 1, expYear: 2020, holderName: "Nome no Cartaao",
                                                     isExpiredCreditCard: false, securityCode: "789");

            Models.CreditCardTransaction creditCardTransaction = null;

            //Act
            BadRequestResult actionResult = (BadRequestResult)transactionCardController.Post(creditCardTransaction).Result;

            //Assert
            Assert.AreEqual(400, actionResult.StatusCode);
        }

        [TestMethod]
        public void Should_Post_TransactionCreditCardController_WithCreditCardTransaction_ReturWithout_CreditCardSaleResponse_Fail()
        {
            //Arrange
            Mock<IFinancialTransactionApp> financialTransactionAppMock = new Mock<IFinancialTransactionApp>();
            var transactionCardController = new TransactionCardController(financialTransactionAppMock.Object);
            var creditCard = UtilTest.FakeCreditCard(creditCardBrand: "Visa", creditCardNumber: "1234", expMonth: 1, expYear: 2020, holderName: "Nome no Cartaao",
                                                     isExpiredCreditCard: false, securityCode: "789");

            var creditCardTransaction = UtilTest.FakeCreditCardTransaction(merchantKey: "10", ammount: 7000, creditCardBrand: "Visa", creditCardNumber: "1234", expMonth: 1,
                                                                           expYear: 2020, holderName: "Nome SA", securityCode: "456");

            var creditCardTransactionResultCollection = new List<CreditCardTransactionResult> { UtilTest.FakeCreditCardTransactionResult(acquirerMessage: "Success", success: true, transactionKey: "2") };
            CreditCardSaleResponse creditCardSaleResponse = null;

            financialTransactionAppMock.Setup(a => a.DoSaleWithCreditCardTransaction(It.IsAny<SaleByCreditCard>())).ReturnsAsync(creditCardSaleResponse);

            //Act
            ObjectResult actionResult = (ObjectResult)transactionCardController.Post(creditCardTransaction).Result;
            var errorReturn = (Error)actionResult.Value;

            //Assert
            financialTransactionAppMock.Verify(v => v.DoSaleWithCreditCardTransaction(It.IsAny<SaleByCreditCard>()), Times.Once);
            Assert.AreEqual(403, actionResult.StatusCode);
            Assert.AreEqual("Não foi possível realizar a venda.", errorReturn.ErrorDescription);
            Assert.AreEqual((HttpStatusCode)403, errorReturn.HttpStatusCode);
        }

        [TestMethod]
        public void Should_Post_TransactionCreditCardController_WithCreditCardTransaction_Return_ThrowsException_Fail()
        {
            //Arrange
            Mock<IFinancialTransactionApp> financialTransactionAppMock = new Mock<IFinancialTransactionApp>();
            var transactionCardController = new TransactionCardController(financialTransactionAppMock.Object);
            var creditCard = UtilTest.FakeCreditCard(creditCardBrand: "Visa", creditCardNumber: "1234", expMonth: 1, expYear: 2020, holderName: "Nome no Cartaao",
                                                     isExpiredCreditCard: false, securityCode: "789");

            var creditCardTransaction = UtilTest.FakeCreditCardTransaction(merchantKey: "10", ammount: 7000, creditCardBrand: "Visa", creditCardNumber: "1234", expMonth: 1,
                                                                           expYear: 2020, holderName: "Nome SA", securityCode: "456");

            var creditCardTransactionResultCollection = new List<CreditCardTransactionResult> { UtilTest.FakeCreditCardTransactionResult(acquirerMessage: "Success", success: true, transactionKey: "2") };

            financialTransactionAppMock.Setup(a => a.DoSaleWithCreditCardTransaction(It.IsAny<SaleByCreditCard>())).Throws(new Exception());

            //Act
            ObjectResult actionResult = (ObjectResult)transactionCardController.Post(creditCardTransaction).Result;
            var errorReturn = (Error)actionResult.Value;

            //Assert
            financialTransactionAppMock.Verify(v => v.DoSaleWithCreditCardTransaction(It.IsAny<SaleByCreditCard>()), Times.Once);
            Assert.AreEqual(500, actionResult.StatusCode);
            Assert.AreEqual("Erro no servidor", errorReturn.ErrorDescription);
            Assert.AreEqual((HttpStatusCode)500, errorReturn.HttpStatusCode);
        }


    }
}
