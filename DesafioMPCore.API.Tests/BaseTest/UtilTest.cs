using DesafioMPCore.Domain.CheckOut;
using DesafioMPCore.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMPCore.API.Tests.BaseTest
{
    public class UtilTest
    {

        #region User

        public static Models.User FakeUser(string userName = "userName 1", string password = "123") =>
            new Models.User
            {
                UserName = userName,
                Password = password,
            };

        #endregion

        #region Token

        public static Token FakeToken(string accessToken = "1", string customerKey = "2", string name = "Test Mock", string userId = "3", string username = "Name Test Mock") => new Token
        {
            AccessToken = accessToken,
            CustomerKey = customerKey,
            Name = name,
            UserId = userId,
            Username = username,
        };

        #endregion

        #region Merchant

        public static Merchant FakeMerchant(string merchantKey = "A013EC31", string publicMerchantKey = "B924EC31", bool isEnabled = true, bool isDeleted = false,
                                    string merchantStatus = "Approved") =>
           new Merchant
           {
               MerchantKey = merchantKey,
               PublicMerchantKey = publicMerchantKey,
               IsEnabled = isEnabled,
               IsDeleted = isDeleted,
               MerchantStatus = merchantStatus
           };

        #endregion

        #region CreditCard

        public static CreditCardSaleResponse FakeCreditCardSaleResponse(string merchantKey = "10",  string buyerKey = "1", List<CreditCardTransactionResult> creditCardTransactionResultCollection = null,
                                                                      string errorReport = "", int internalTime = 0) =>
          new CreditCardSaleResponse
          {
              BuyerKey = buyerKey,
              CreditCardTransactionResultCollection = creditCardTransactionResultCollection,
              ErrorReport = errorReport,
              InternalTime = internalTime,
              MerchantKey = merchantKey
          };

        public static CreditCardTransactionResult FakeCreditCardTransactionResult(string acquirerMessage = "Success", bool success = true, string transactionKey = "1234") =>
            new CreditCardTransactionResult
            {
                AcquirerMessage = acquirerMessage,
                Success = success,
                TransactionKey = transactionKey,
            };

        public static Models.CreditCardTransaction FakeCreditCardTransaction(string merchantKey = "10", decimal ammount = 1, string buyerEmail = "email@email.com", string buyerName = "Nome Comprador",
                                                                             string creditCardBrand = "Visa", string creditCardNumber = "1234", int expMonth = 1,
                                                                             int expYear = 2020, string holderName = "Nome no Cartao", string securityCode = "456") =>
           new Models.CreditCardTransaction
           {
               Ammount= ammount,
               BuyerEmail = buyerEmail,
               BuyerName = buyerName,
               CreditCardBrand = creditCardBrand,
               CreditCardNumber = creditCardNumber,
               ExpMonth = expMonth,
               ExpYear = expYear,
               HolderName = holderName,
               MerchantKey = merchantKey,
               SecurityCode = securityCode,
           };

        public static CreditCard FakeCreditCard(string creditCardBrand = "Visa", string creditCardNumber = "1234567890123456", int expMonth = 1, int expYear = 2020,
                                                string holderName = "Nome S A", bool isExpiredCreditCard = false, string securityCode = "789") =>
            new CreditCard
            {
                CreditCardBrand = creditCardBrand,
                CreditCardNumber = creditCardNumber,
                ExpMonth = expMonth,
                ExpYear = expYear,
                HolderName = holderName,
                IsExpiredCreditCard = isExpiredCreditCard,
                SecurityCode = securityCode
            };

        #endregion

    }
}
