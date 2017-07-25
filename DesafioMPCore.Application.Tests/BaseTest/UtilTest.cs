using DesafioMPCore.Domain.CheckOut;
using DesafioMPCore.Domain.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace DesafioMPCore.Application.Tests.BaseTest
{
    public class UtilTest
    {
        #region Json

        public static string SerializeObject(Object value)
        {
            return JsonConvert.SerializeObject(value,
                                                      Newtonsoft.Json.Formatting.None,
                                                      new JsonSerializerSettings
                                                      {
                                                          NullValueHandling = NullValueHandling.Ignore
                                                      });
        }

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

        public static string FakeTokenJson(string accessToken = "1", string customerKey = "2", string name = "Test Mock", string userId = "3", string username = "Name Test Mock")
        {
            var token = UtilTest.FakeToken(accessToken: accessToken, customerKey: customerKey, name: name, userId: userId, username: username);

            var tokenJson = JsonConvert.SerializeObject(token,
                                                      Newtonsoft.Json.Formatting.None,
                                                      new JsonSerializerSettings
                                                      {
                                                          NullValueHandling = NullValueHandling.Ignore
                                                      });

            return tokenJson;
        }

        #endregion

        #region User

        public static User FakeUser(string userName = "teste", string password = "123456") => new User
        {
            UserName = userName,
            Password = password
        };

        #endregion

        #region Fake HttpContent

        public static HttpResponseMessage FakeHttpResponseMessage(string contentJson, int httpStatusCode)
        {
            var content = new StringContent(contentJson, Encoding.UTF8, "application/json");
            var fakeHttpResponseMessage = new HttpResponseMessage();

            fakeHttpResponseMessage.Content = content;
            fakeHttpResponseMessage.StatusCode = (HttpStatusCode)httpStatusCode;

            return fakeHttpResponseMessage;
        }

        #endregion

        #region Token

        public static SaleByCreditCard FakeToken(string merchantKey = "1", Order order = null, List<CreditCardTransaction> creditCardTransaction = null) =>
            new SaleByCreditCard
            {
                MerchantKey = merchantKey,
                Order = order,
                CreditCardTransactionCollection = creditCardTransaction,
            };

        #endregion

        #region CreditCard

        public static CreditCardTransaction FakeCreditCardTransaction(long amountInCents, CreditCard creditCard) =>
            new CreditCardTransaction
            {
                AmountInCents = amountInCents,
                CreditCard = creditCard
            };

        public static string FakeCreditCardTransactionJson(long amountInCents, CreditCard creditCard)
        {
            var creditCardTransaction = FakeCreditCardTransaction(amountInCents: amountInCents, creditCard: creditCard);

            return UtilTest.SerializeObject(creditCardTransaction);
        }

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

        public static string FakeCreditCardJson(string creditCardBrand = "Visa", string creditCardNumber = "1234567890123456", int expMonth = 1, int expYear = 2020,
                                                string holderName = "Nome S A", bool isExpiredCreditCard = false, string securityCode = "789")
        {
            var creditCard = UtilTest.FakeCreditCard(
                creditCardBrand: creditCardBrand,
                creditCardNumber: creditCardNumber,
                expMonth: expMonth,
                expYear: expYear,
                holderName: holderName,
                isExpiredCreditCard: isExpiredCreditCard,
                securityCode: securityCode
            );

            return UtilTest.SerializeObject(creditCard);
        }

        public static SaleByCreditCard FakeSaleByCreditCard(string merchantKey = "", List<CreditCardTransaction> creditCardTransactionCollection = null, Order order = null) =>
            new SaleByCreditCard
            {
                MerchantKey = merchantKey,
                CreditCardTransactionCollection = creditCardTransactionCollection,
                Order = order
            };

        public static CreditCardSaleResponse FakeCreditCardSaleResponse(string buyerKey = "1", List<CreditCardTransactionResult> creditCardTransactionResultCollection = null,
                                                                        string errorReport = "" , int internalTime = 0) =>
            new CreditCardSaleResponse
            {
                BuyerKey = buyerKey,
                CreditCardTransactionResultCollection = creditCardTransactionResultCollection,
                ErrorReport = errorReport,
                InternalTime = internalTime,
            };

        public static CreditCardTransactionResult FakeCreditCardTransactionResult(string acquirerMessage = "Success", bool success = true, string transactionKey = "1234") =>
            new CreditCardTransactionResult
            {
                AcquirerMessage = acquirerMessage,
                Success = success,
                TransactionKey = transactionKey,
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

        public static MerchantResponse FakeMerchantResponse(List<Merchant> items) =>
          new MerchantResponse
          {
              Items = items
          };

        #endregion


    }
}
