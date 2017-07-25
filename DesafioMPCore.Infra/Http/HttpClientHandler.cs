using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DesafioMPCore.Infra.Http
{
    public class HttpClientHandler : IHttpHandler
    {
        public HttpClientHandler()
        {
            ((IHttpHandler)this).HttpClient.DefaultRequestHeaders.Accept.Clear();
            ((IHttpHandler)this).HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        HttpClient IHttpHandler.HttpClient { get; set; } = new HttpClient();

        void IHttpHandler.Dispose()
        {
            ((IHttpHandler)this).HttpClient.Dispose();
        }

        HttpResponseMessage IHttpHandler.Get(string url)
        {
            return ((IHttpHandler)this).GetAsync(url).Result;
        }

        async Task<HttpResponseMessage> IHttpHandler.GetAsync(string url)
        {
            return await ((IHttpHandler)this).HttpClient.GetAsync(url);
        }

        Task<HttpResponseMessage> IHttpHandler.GetAsync(string url, HttpContent content)
        {
            throw new NotImplementedException();
        }

        HttpResponseMessage IHttpHandler.Post(string url, HttpContent content)
        {
            return ((IHttpHandler)this).PostAsync(url, content).Result;
        }

        async Task<HttpResponseMessage> IHttpHandler.PostAsync(string url, HttpContent content)
        {
            return await ((IHttpHandler)this).HttpClient.PostAsync(url, content);

        }
    }
}
