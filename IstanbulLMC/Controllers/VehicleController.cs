using IstanbulLMC.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IstanbulLMC.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IConfiguration _configuration;

        public VehicleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult VehicleList()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VehicleList(TransferDTO transferDTO)
        {
            string apiKey = _configuration["MapsKey"] ?? "";

            string baseUrl = "https://maps.googleapis.com/maps/api/directions/json";

            string originPlaceId = transferDTO.FromPlaceID ?? "";
            string destinationPlaceId = transferDTO.ToPlaceID ?? "";

            string url = $"{baseUrl}?origin=place_id:{originPlaceId}&destination=place_id:{destinationPlaceId}&key={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    DirectionsDTO directions = JsonConvert.DeserializeObject<DirectionsDTO>(jsonResponse) ?? new();

                    int distance = directions.routes[0].legs[0].distance.value;
                }
                else
                {
                }
            }
            return View();
        }
    }
}
