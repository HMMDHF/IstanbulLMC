using IstanbulLMC.DTOs;
using IstanbulLMC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace IstanbulLMC.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly lmcTourismContext db;

        public VehicleController(IConfiguration configuration, lmcTourismContext context)
        {
            _configuration = configuration;
            db = context;
        }

        public IActionResult VehicleList()
        {
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> VehicleList(TransferDTO transferDTO)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://maps.googleapis.com/maps/api/directions/json?origin=place_id:{transferDTO.FromPlaceID ?? ""}&destination=place_id:{transferDTO.ToPlaceID ?? ""}&key={_configuration["MapsKey"] ?? ""}");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    DirectionsDTO directions = JsonConvert.DeserializeObject<DirectionsDTO>(jsonResponse) ?? new();

                    decimal distance = Convert.ToDecimal(Math.Ceiling((double)directions.routes[0].legs[0].distance.value / 1000));

                    transferDTO.Distance = distance;
                    transferDTO.Duration = directions.routes[0].legs[0].duration.text;
                    transferDTO.VehicleCategories = await db.VehicleCategory.Where(x => x.IsActive && x.MaxDistance >= distance && x.SeateCount >= transferDTO.PassengersCount).Select(x => new VehicleCategoryDTO
                    {
                        ID = x.ID,
                        Name = x.Name,
                        KMPrice = x.KMPrice,
                        Image = x.Image,
                        SeateCount = x.SeateCount,
                        SuitcaseCount = x.SeateCount,
                    }).ToListAsync();

                    return View(transferDTO);
                }
                else
                {
                }
            }
            return View();
        }
    }
}
