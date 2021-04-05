using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TIDEFloodMonitoring.Models;
using TIDEFloodMonitoring.Models.Configuration;
using TIDEFloodMonitoring.Service.Interface;

namespace TIDEFloodMonitoring.Service
{
    public class BusinessService : IBusinessService
    {
        private readonly IApiService _apiService;
        private readonly APIConfiguration _config;

        public BusinessService(IApiService apiService, IOptionsSnapshot<APIConfiguration> config)
        {
            _apiService = apiService;
            _config = config.Value;
        }

        public async Task<FloodResponse> GetFloodWarning(string county)
        {
            var message = "N/A";

            try
            {
                var response = await _apiService.Get(_config.FloodBaseUrl, "/id/floods", $"?county={county}");
                if (response.IsSuccessStatusCode)
                {
                    var detail = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<FloodResponse>(detail);
                }

                message = $"Unsuccessfull Request - {await response.Content.ReadAsStringAsync()}";
            }
            catch (Exception ex)
            {
                message = $"Exception: {ex.InnerException} - Message {ex.Message}";
            }

            return new FloodResponse(message);
        }
    }
}
