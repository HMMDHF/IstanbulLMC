using IstanbulLMC.Areas.Admin.DTOs;
using IstanbulLMC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Base;

namespace IstanbulLMC.Areas.Admin.Controllers
{
    public class ServiceController : Controller
    {
        private readonly lmcTourismContext _context;
        public ServiceController(lmcTourismContext context)
        {
            _context = context;
        }

        [CustomAuthorize]
        public IActionResult ServiceList()
        {
            return View();
        }

        [CustomAuthorize]
        public ActionResult GetServiceList([FromBody] DataManagerRequest dm)
        {
            IQueryable<Service> DataSource = _context.Service;

            QueryableOperation operation = new QueryableOperation();
            if (dm.Where != null)
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Condition);
            }
            if (dm.Search != null)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);
            }
            int count = DataSource.Cast<object>().Count();
            if (dm.Sorted != null)
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Select != null)
            {
                DataSource = (IQueryable<Service>)operation.PerformSelect(DataSource, dm.Select);
            }
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }

        [CustomAuthorize]
        public async Task<IActionResult> SaveService([FromBody] SyncfusionGridDTO<Service> updatedData)
        {
            try
            {
                if (updatedData.value.ID == 0)
                {
                    await _context.Service.AddAsync(updatedData.value);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var vehicleCategory = await _context.Service.AsNoTracking().FirstOrDefaultAsync(x => x.ID == updatedData.value.ID);
                    if (vehicleCategory == null)
                    {
                        return RedirectToAction("MyTripInfo");
                    }

                    _context.Service.Update(updatedData.value);
                    await _context.SaveChangesAsync();
                }

                return Json(updatedData.value); // Adjust the action and controller names accordingly

            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Error updating record" });
            }
            // If ModelState is not valid, return a JSON response with validation errors
            var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return Json(new { success = false, message = "Validation error", errors = errors });
        }
    }
}
