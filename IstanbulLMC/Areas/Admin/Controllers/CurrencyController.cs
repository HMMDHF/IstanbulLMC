using IstanbulLMC.Areas.Admin.DTOs;
using IstanbulLMC.Areas.Admin.Models;
using IstanbulLMC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Base;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulLMC.Areas.Admin.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly lmcTourismContext _context;

        public CurrencyController(lmcTourismContext context)
        {
            _context = context;
        }

        [CustomAuthorize]
        public IActionResult CurrencyList()
        {
            return View();
        }

        [CustomAuthorize]
        public ActionResult GetCurrencyList([FromBody] DataManagerRequest dm)
        {
            IQueryable<Currency> dataSource = _context.Currency;

            QueryableOperation operation = new QueryableOperation();
            if (dm.Where != null)
            {
                dataSource = operation.PerformFiltering(dataSource, dm.Where, dm.Where[0].Condition);
            }
            if (dm.Search != null)
            {
                dataSource = operation.PerformSearching(dataSource, dm.Search);
            }
            int count = dataSource.Cast<object>().Count();
            if (dm.Sorted != null)
            {
                dataSource = operation.PerformSorting(dataSource, dm.Sorted);
            }
            if (dm.Select != null)
            {
                dataSource = (IQueryable<Currency>)operation.PerformSelect(dataSource, dm.Select);
            }
            if (dm.Skip != 0)
            {
                dataSource = operation.PerformSkip(dataSource, dm.Skip);
            }
            if (dm.Take != 0)
            {
                dataSource = operation.PerformTake(dataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = dataSource, count = count }) : Json(dataSource);
        }

        [CustomAuthorize]
        public async Task<IActionResult> SaveCurrency([FromBody] SyncfusionGridDTO<Currency> updatedData)
        {
            try
            {
                if (updatedData.value.Id == 0)
                {
                    await _context.Currency.AddAsync(updatedData.value);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var existingCurrency = await _context.Currency.AsNoTracking().FirstOrDefaultAsync(x => x.Id == updatedData.value.Id);
                    if (existingCurrency == null)
                    {
                        return RedirectToAction("CurrencyList");
                    }

                    _context.Currency.Update(updatedData.value);
                    await _context.SaveChangesAsync();
                }

                return Json(updatedData.value);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Error updating record" });
            }
        }
    }
}
