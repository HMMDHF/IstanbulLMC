using IstanbulLMC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IstanbulLMC.Areas.Admin.Controllers
{
    public class TransferController : Controller
    {
        private readonly lmcTourismContext _context;
        public TransferController(lmcTourismContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> TransferList()
        {
            return View(await _context.Transfer.ToListAsync()); 
        }
    }
}
