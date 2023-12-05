using IstanbulLMC.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace IstanbulLMC.Controllers
{
    public class CustomerController : Controller
    {

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CustomerApplication(VehicleCategoryDTO model,int carId)
        {

            return View(model); // or handle the error
        }

    }
}
