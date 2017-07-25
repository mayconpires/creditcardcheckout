using DesafioMPCore.Domain.Interface.Application;
using System;
using System.Collections.Generic;
using System.Text;
using DesafioMPCore.Domain.Shared;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using DesafioMPCore.Infra.Http;

namespace DesafioMPCore.Application.CheckOut
{
    public class AccessTokenApp : IAccessTokenApp
    {
        readonly string _userAccessTokenServiceUrl;
        IHttpHandler _httpHandler;

        public AccessTokenApp(IHttpHandler httpHandler, string userAccessTokenServiceUrl)
        {
            _httpHandler = httpHandler;
            _userAccessTokenServiceUrl = userAccessTokenServiceUrl;
        }

        async Task<Token> IAccessTokenApp.AuthenticateToCreateUserAccessToken(User user)
        {
            var userJson = JsonConvert.SerializeObject(user,
                                                  Newtonsoft.Json.Formatting.None,
                                                  new JsonSerializerSettings
                                                  {
                                                      NullValueHandling = NullValueHandling.Ignore
                                                  });

            var httpResponseMessageTask = _httpHandler.PostAsync(_userAccessTokenServiceUrl, new StringContent(userJson, Encoding.UTF8, "application/json"));
            var httpResponseMessage = await httpResponseMessageTask;
            httpResponseMessage.EnsureSuccessStatusCode();

            var userAccessTokenResponseString = await httpResponseMessage.Content.ReadAsStringAsync();
            _httpHandler.Dispose();
            var accessToken = JsonConvert.DeserializeObject<Token>(userAccessTokenResponseString);


            return accessToken;
        }

    }
}
