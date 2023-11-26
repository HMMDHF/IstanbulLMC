using IstanbulLMC.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IstanbulLMC.Controllers
{
    public class VehicleController : Controller
    {

        [HttpGet]
        public async Task<IActionResult> VehicleList(TransferDTO transferDTO)
        {
            string apiKey = "YOUR_API_KEY";
            string baseUrl = "https://maps.googleapis.com/maps/api/directions/json";

            string originPlaceId = "YOUR_ORIGIN_PLACE_ID";
            string destinationPlaceId = "YOUR_DESTINATION_PLACE_ID";

            string url = $"{baseUrl}?origin=place_id:{originPlaceId}&destination=place_id:{destinationPlaceId}&key={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    dynamic directions = JsonConvert.DeserializeObject(jsonResponse) ?? new object();

                    string distance = directions.routes[0].legs[0].distance.text;
                }
                else
                {
                }
            }
            return View();
        }
    }
}
