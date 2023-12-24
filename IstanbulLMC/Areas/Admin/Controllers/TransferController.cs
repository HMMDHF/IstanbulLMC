using IstanbulLMC.Areas.Admin.DTOs;
using IstanbulLMC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Base;

namespace IstanbulLMC.Areas.Admin.Controllers
{
    public class TransferController : Controller
    {
        private readonly lmcTourismContext _context;
        public TransferController(lmcTourismContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult TransferList()
        {
            return View();
        }

        public ActionResult GetTransferList([FromBody] DataManagerRequest dm)
        {
            IQueryable<TransferDTO> DataSource = _context.Transfer.Select(x => new TransferDTO
            {
                ID = x.ID,
                IsActive = x.IsActive,
                Name = x.Name,
                InsertID = x.InsertID,
                InsertDate = x.InsertDate,
                KMPrice = x.KMPrice,
                Distance = x.Distance,
                FlieghtNo = x.FlieghtNo,
                FromPlace = x.FromPlace,
                FromPlaceID = x.FromPlaceID,
                IsComplated = x.IsComplated,
                Message = x.Message,
                NO = x.NO,
                Tel = x.Tel,
                ToPlace = x.ToPlace,
                ToPlaceID = x.ToPlaceID,
                TotalPrice = x.TotalPrice,
                VehicleCategoryID = x.VehicleCategoryID,
                VehicleCategory = x.VehicleCategory.Name
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
                DataSource = (IQueryable<TransferDTO>)operation.PerformSelect(DataSource, dm.Select);
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

        public async Task<IActionResult> SaveTransfer([FromBody] SyncfusionGridDTO<TransferDTO> updatedData)
        {
            try
            {
                if (updatedData.value.ID == 0)
                {
                    //await _context.Transfer.AddAsync(updatedData.value);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var Transfer = await _context.Transfer.AsNoTracking().FirstOrDefaultAsync(x => x.ID == updatedData.value.ID);
                    if (Transfer == null)
                    {
                        return RedirectToAction("MyTripInfo");
                    }

                    //_context.Transfer.Update(updatedData.value);
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
