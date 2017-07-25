using DesafioMPCore.Domain.Interface.Application;
using System;
using System.Collections.Generic;
using System.Text;
using DesafioMPCore.Domain.Shared;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using DesafioMPCore.Domain.CheckOut;
using DesafioMPCore.Infra.Http;

namespace DesafioMPCore.Application.CheckOut
{
    public class MerchantApp : IMerchantApp
    {
        IHttpHandler _httpHandler;
        readonly string _merchantGetUrl;

        public MerchantApp(IHttpHandler httpHandler, string merchantGetUrl)
        {
            _httpHandler = httpHandler;
            _merchantGetUrl = merchantGetUrl;
        }

        async Task<List<Merchant>> IMerchantApp.SeekMerchants(string userId)
        {
            var merchantGetUrlFormatted = string.Format(_merchantGetUrl, userId);

            var httpResponseMessageTask = _httpHandler.GetAsync(merchantGetUrlFormatted);
            var httpResponseMessage = await httpResponseMessageTask;
            httpResponseMessage.EnsureSuccessStatusCode();
            var userAccessTokenResponseString = await httpResponseMessage.Content.ReadAsStringAsync();
            var merchantResponse = JsonConvert.DeserializeObject<MerchantResponse>(userAccessTokenResponseString);

            _httpHandler.Dispose();

            return merchantResponse.Items;
        }

    }
}
