using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TIDEFloodMonitoring.Models;
using TIDEFloodMonitoring.Service.Interface;

namespace TIDEFloodMonitoring.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBusinessService _businessService;
        private readonly IViewRenderService _viewRenderService;
        private readonly ILogger<HomeController> _log;

        public HomeController(ILogger<HomeController> logger, IBusinessService businessService, IViewRenderService viewRenderService, ILogger<HomeController> log)
        {
            _logger = logger;
            _businessService = businessService;
            _viewRenderService = viewRenderService;
            _log = log;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetFloodWarning([FromQuery] string county)
        {
            _log.LogInformation("Request Made");

            if (string.IsNullOrWhiteSpace(county))
            {
                _log.LogWarning("Invalid County");
                return Json(new { success = false, message = "Invalid County" });
            }

            var response = await _businessService.GetFloodWarning(county);

            _log.LogInformation(JsonConvert.SerializeObject(response));

            var vM = new FloodWarningViewModel()
            {
                FloodResponse = response
            };

            var html = await _viewRenderService.RenderToStringAsync("~/Views/Shared/_ResultCard.cshtml", vM);
            return Json(new { success = true, detail = html });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
