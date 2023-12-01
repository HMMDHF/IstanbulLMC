using Microsoft.AspNetCore.Mvc;

namespace IstanbulLMC.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult CustomerApplication()
        {
            return View();
        }
    }
}
