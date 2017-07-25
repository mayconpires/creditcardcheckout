using DesafioMPCore.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using DesafioMPCore.Domain.CheckOut;
using DesafioMPCore.Domain.Interface.Application;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DesafioMPCore.Infra.Http;

namespace DesafioMPCore.Application.CheckOut
{
    public class FinancialTransactionApp : IFinancialTransactionApp
    {
        IHttpHandler _httpHandler;
        readonly string _saleUrl;

        public FinancialTransactionApp(IHttpHandler httpHandler, string saleUrl)
        {
            _httpHandler = httpHandler;
            _saleUrl = saleUrl;
        }

        async Task<CreditCardSaleResponse> IFinancialTransactionApp.DoSaleWithCreditCardTransaction(SaleByCreditCard saleByCreditCard)
        {
            _httpHandler.HttpClient.DefaultRequestHeaders.Add("MerchantKey", saleByCreditCard.MerchantKey);

            var transactionCardJson = JsonConvert.SerializeObject(saleByCreditCard,
                                                                  Newtonsoft.Json.Formatting.None,
                                                                  new JsonSerializerSettings
                                                                  {
                                                                      NullValueHandling = NullValueHandling.Ignore
                                                                  });

            var httpResponseMessageTask = _httpHandler.PostAsync(_saleUrl, new StringContent(transactionCardJson, Encoding.UTF8, "application/json"));
            var httpResponseMessage = await httpResponseMessageTask;
            httpResponseMessage.EnsureSuccessStatusCode();
            var creditCardSaleResponseString = await httpResponseMessage.Content.ReadAsStringAsync();
            _httpHandler.Dispose();
            var creditCardSaleResponse = JsonConvert.DeserializeObject<CreditCardSaleResponse>(creditCardSaleResponseString);

            return creditCardSaleResponse;
        }

        
    }
}
