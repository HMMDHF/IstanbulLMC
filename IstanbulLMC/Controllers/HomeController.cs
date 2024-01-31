using Azure;
using IstanbulLMC.Areas.Admin.DTOs;
using IstanbulLMC.DTOs;
using IstanbulLMC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Net;

namespace IstanbulLMC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly lmcTourismContext db;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, lmcTourismContext context)
        {
            _configuration = configuration;
            _logger = logger;
            db = context;
        }
        

        public IActionResult Index()
        {
            ViewData["maxSeateCount"] = db.VehicleCategory.Max(x => x.SeateCount);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetPlaceByName(string name, string GoogleCaptchToken)
        {
            if (await RecaptchaService.IsCapchaValid(GoogleCaptchToken, this.HttpContext.Connection.RemoteIpAddress.ToString()))
            {
                string apiKey = "80305a30-48dc-49f7-9397-d5a45d5347c0"; // يجب عليك استبداله بمفتاح API الخاص بك

                string apiUrl = $"https://search-maps.yandex.ru/v1/?text={WebUtility.UrlEncode(name)}&type=geo&lang=en_US&apikey={apiKey}";

                WebClient client = new WebClient();
                string response = client.DownloadString(apiUrl);

                //JObject jsonResponse = JObject.Parse(response);

                YandexApiResponse yandexApiResponse = JsonConvert.DeserializeObject<YandexApiResponse>(response);

                List<SubApiResponse> topFivePlaces = yandexApiResponse.features.Select(x => new SubApiResponse {
                    Name = x.properties.name,
                }).ToList();

                return PartialView("_SearchResultsPartial", topFivePlaces);
                
            }
            return PartialView("_SearchResultsPartial", new List<SubApiResponse>()); // Return an error view or appropriate response
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