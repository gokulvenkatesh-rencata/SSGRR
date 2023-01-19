using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DigiFamily.Interfaces
{
    public interface IHttpClientService
    {
        Task<T> GetAsync<T>(string url);
        Task<T> PostAsync<T>(string url, HttpContent content);
        Task<T> AnonymousGetAsync<T>(string url);
        Task<T> AnonymousPostAsync<T>(string url, HttpContent content);
    }
}
