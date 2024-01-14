using IstanbulLMC.DTOs;
using IstanbulLMC.MicroServices;
using IstanbulLMC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace IstanbulLMC.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly lmcTourismContext db;
        private readonly SessionService _sessionService;

        public VehicleController(IConfiguration configuration, lmcTourismContext context, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            db = context;
            _sessionService = new SessionService(httpContextAccessor);
        }

        public IActionResult VehicleList()
        {
            TransferDTO transferDTO = _sessionService.GetTransferDTO();
            if (transferDTO != null)
            {
                return View(transferDTO);
            }
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> VehicleList(TransferDTO transferDTO)
        {
            if (await RecaptchaService.IsCapchaValid(transferDTO.GoogleCaptchToken, this.HttpContext.Connection.RemoteIpAddress.ToString()))
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

                        _sessionService.SetTransferSession(transferDTO);
                        return View(transferDTO);
                    }
                    else
                    {
                    }
                }
                return View();
            }
            return Redirect("/");
        }
    }
}
