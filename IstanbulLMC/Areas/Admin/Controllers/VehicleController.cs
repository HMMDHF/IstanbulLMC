using IstanbulLMC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IstanbulLMC.Areas.Admin.Controllers
{
    public class VehicleController : Controller
    {
        private readonly lmcTourismContext _context;
        public VehicleController(lmcTourismContext context)
        {
            _context = context;
        }

        [CustomAuthorize]
        public async Task<IActionResult> VehicleCategoryList()
        {
            return View(await _context.VehicleCategory.ToListAsync());
        }
    }
}
