using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TIDEFloodMonitoring.Models;

namespace TIDEFloodMonitoring.Service.Interface
{
    public interface IBusinessService
    {
        Task<FloodResponse> GetFloodWarning(string county);
    }
}
