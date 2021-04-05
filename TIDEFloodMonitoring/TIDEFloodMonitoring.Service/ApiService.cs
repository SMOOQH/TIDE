using System;
using System.Net.Http;
using System.Threading.Tasks;
using TIDEFloodMonitoring.Service.Interface;

namespace TIDEFloodMonitoring.Service
{

    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> Get(string baseUrl, string endpoint)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                return await client.GetAsync($"{baseUrl}/{endpoint}");
            }
        }

        public async Task<HttpResponseMessage> Get(string baseUrl, string endpoint, string queryValues)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                return await client.GetAsync($"{baseUrl}{endpoint}{queryValues}");
            }
        }

    }
}
