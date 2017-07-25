using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DesafioMPCore.Infra.Http
{
    public interface IHttpHandler
    {
        HttpClient HttpClient { get; set; }
        HttpResponseMessage Post(string url, HttpContent content);
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
        HttpResponseMessage Get(string url);
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> GetAsync(string url, HttpContent content);
        void Dispose();
    }
}
