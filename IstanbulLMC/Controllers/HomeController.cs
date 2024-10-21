using IstanbulLMC.Areas.Admin.DTOs;
using IstanbulLMC.DTOs;
using IstanbulLMC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Diagnostics.Metrics;

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
                string apiKey = _configuration["MapsKey"] ?? "";
                string baseUrl = "https://maps.googleapis.com/maps/api/place/textsearch/json";

                string location = "Istanbul"; // اسم المدينة أو الإحداثيات الجغرافية للموقع المراد البحث عن الأماكن فيه
                string keyword = name; // الاسم الذي تريد البحث عنه
                
                string types = "airport|hotel|tourist_attraction|square"; // قائمة أنواع الأماكن المفصولة بعلامة الخطوط الرأسية "|"
                
                string encodedKeyword = Uri.EscapeDataString(keyword);// استخدم URLEncoder لترميز النص إذا كان يحتوي على مسافات أو رموز خاصة

                string url = $"{baseUrl}?query={encodedKeyword}&components=country:TR&key={apiKey}";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        var jsonResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);

                        var topFivePlaces = jsonResponse.subApiResponses.ToList();
                        return PartialView("_SearchResultsPartial", topFivePlaces);
                    }
                }
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