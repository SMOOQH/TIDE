using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TIDEFloodMonitoring.Service.Interface
{
    public interface IApiService
    {
        Task<HttpResponseMessage> Get(string baseUrl, string endpoint);
        Task<HttpResponseMessage> Get(string baseUrl, string endpoint, string queryValues);
    }
}
