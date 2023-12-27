using IstanbulLMC.Areas.Admin.DTOs;
using IstanbulLMC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Base;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IstanbulLMC.Areas.Admin.Controllers
{
    public class TransferController : Controller
    {
        private readonly lmcTourismContext _context;
        public TransferController(lmcTourismContext context)
        {
            _context = context;
        }

        [CustomAuthorize]
        public IActionResult TransferList()
        {
            return View();
        }

        [CustomAuthorize]
        public ActionResult GetTransferList([FromBody] DataManagerRequest dm)
        {
            IQueryable<TransferDTO> DataSource = _context.Transfer.Select(x => new TransferDTO
            {
                ID = x.ID,
                IsActive = x.IsActive,
                Name = x.Name,
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
                KMPrice = x.KMPrice,
                VehicleCategoryID = x.VehicleCategoryID,
                VehicleCategory = x.VehicleCategory.Name,
                Date = x.Date,
                RoundTripDate = x.RoundTripDate
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

        [CustomAuthorize]
        public async Task<IActionResult> SaveTransfer([FromBody] SyncfusionGridDTO<TransferDTO> updatedData)
        {
            try
            {
                if (updatedData.value.ID == 0)
                {
                    await _context.Transfer.AddAsync(new Transfer
                    {
                        Distance = updatedData.value.Distance,
                        FlieghtNo = updatedData.value.FlieghtNo,
                        FromPlace = updatedData.value.FromPlace,
                        FromPlaceID = updatedData.value.FromPlaceID,
                        IsActive = updatedData.value.IsActive,
                        IsComplated = updatedData.value.IsComplated,
                        KMPrice = updatedData.value.KMPrice,
                        Message = updatedData.value.Message,
                        Name = updatedData.value.Name,
                        NO = updatedData.value.NO,
                        Tel = updatedData.value.Tel,
                        ToPlace = updatedData.value.ToPlace,
                        ToPlaceID = updatedData.value.ToPlaceID,
                        TotalPrice = updatedData.value.TotalPrice,
                        VehicleCategoryID = updatedData.value.VehicleCategoryID,
                        Date = updatedData.value.Date,
                        RoundTripDate = updatedData.value.RoundTripDate
                    });
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var transfer = await _context.Transfer.AsNoTracking().FirstOrDefaultAsync(x => x.ID == updatedData.value.ID);
                    if (transfer == null)
                    {
                        return RedirectToAction("MyTripInfo");
                    }

                    transfer.IsComplated = updatedData.value.IsComplated;
                    transfer.IsActive = updatedData.value.IsActive;
                    transfer.Distance = updatedData.value.Distance;
                    transfer.FlieghtNo = updatedData.value.FlieghtNo;
                    transfer.FromPlace = updatedData.value.FromPlace;
                    transfer.FromPlaceID = updatedData.value.FromPlaceID;
                    transfer.ToPlace = updatedData.value.ToPlace;
                    transfer.ToPlaceID = updatedData.value.ToPlaceID;
                    transfer.Message = updatedData.value.Message;
                    transfer.TotalPrice = updatedData.value.TotalPrice;
                    transfer.VehicleCategoryID = updatedData.value.VehicleCategoryID;
                    transfer.KMPrice = updatedData.value.KMPrice;
                    transfer.Name = updatedData.value.Name;
                    transfer.NO = updatedData.value.NO;
                    transfer.Tel = updatedData.value.Tel;
                    transfer.Date = updatedData.value.Date;
                    transfer.RoundTripDate = updatedData.value.RoundTripDate;

                    _context.Transfer.Update(transfer);
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
