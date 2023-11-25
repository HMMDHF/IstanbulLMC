using IstanbulLMC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IstanbulLMC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        

        public async Task<IActionResult> Index()
        {
            //string apiKey = "AIzaSyBM3yphMQm5oDBRuAinvFILcsWI5GYD19w";
            //string baseUrl = "https://maps.googleapis.com/maps/api/place/textsearch/json";

            //string location = ""; // اسم المدينة أو الإحداثيات الجغرافية للموقع المراد البحث عن الأماكن فيه
            //string query = "elit world"; // نوع الأماكن المطلوب البحث عنها

            //string url = $"{baseUrl}?query={query}&location={location}&key={apiKey}";

            //using (HttpClient client = new HttpClient())
            //{
            //    HttpResponseMessage response = await client.GetAsync(url);

            //     if (response.IsSuccessStatusCode)
            //    {
            //        string jsonResponse = await response.Content.ReadAsStringAsync();
            //    }
            //    else
            //    {

            //    }
            //}

            return View();
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