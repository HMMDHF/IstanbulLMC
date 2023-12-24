using IstanbulLMC.Areas.Admin.DTOs;
using IstanbulLMC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Base;
using System.Security.Claims;

namespace IstanbulLMC.Areas.Admin.Controllers
{
    public class VehicleController : Controller
    {
        private readonly lmcTourismContext _context;
        public VehicleController(lmcTourismContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult VehicleCategoryList()
        {
            return View();
        }

        public ActionResult GetVehicleCategoryList([FromBody] DataManagerRequest dm)
        {
            IQueryable<VehicleCategoryDTO> DataSource = _context.VehicleCategory.Select(x => new VehicleCategoryDTO
            {
                ID = x.ID,
                IsActive = x.IsActive,
                Name = x.Name,
                InsertID = x.InsertID,
                Image = x.Image,
                InsertDate = x.InsertDate,
                KMPrice = x.KMPrice,
                MaxDistance = x.MaxDistance,
                SeateCount = x.SeateCount,
                UpdateDate = x.UpdateDate,
                UpdateID = x.UpdateID,
                SuitcaseCount = x.SuitcaseCount
            });

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
                DataSource = (IQueryable<VehicleCategoryDTO>)operation.PerformSelect(DataSource, dm.Select);
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

        public async Task<IActionResult> SaveVehicleCategory([FromBody] SyncfusionGridDTO<VehicleCategory> updatedData)
        {
            try
            {
                if (updatedData.value.ID == 0)
                {
                    await _context.VehicleCategory.AddAsync(updatedData.value);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var vehicleCategory = await _context.VehicleCategory.AsNoTracking().FirstOrDefaultAsync(x => x.ID == updatedData.value.ID);
                    if (vehicleCategory == null)
                    {
                        return RedirectToAction("MyTripInfo");
                    }

                    _context.VehicleCategory.Update(updatedData.value);
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