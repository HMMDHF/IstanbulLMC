using IstanbulLMC.DTOs;
using IstanbulLMC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace IstanbulLMC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly lmcTourismContext db;

        public CustomerController(lmcTourismContext context)
        {
            db = context;
        }

        public IActionResult CustomerApplication()
        {
            return Redirect("/");
        }   

        [HttpPost]
        public async Task<IActionResult> CustomerApplication(TransferDTO transferDTO)
        {
            VehicleCategory vehicleCategory = await db.VehicleCategory.FirstOrDefaultAsync(x => x.ID == transferDTO.VehicleCategoryID) ?? new VehicleCategory();

            transferDTO.Vehicle = vehicleCategory.Name;

            transferDTO.Services = await db.Service.Where(x => x.IsActive).Select(x => new ServiceDTO
            {
                Name = x.Name,
                ID = x.ID,
                IsActive = x.IsActive,
                Price = x.Price,
                Icon = x.Icon,
            }).ToListAsync();
            return View(transferDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CustomerApplicationSave(TransferDTO transferDTO)
        {
            VehicleCategory vehicleCategory = await db.VehicleCategory.FirstOrDefaultAsync(x => x.ID == transferDTO.VehicleCategoryID) ?? new VehicleCategory();

            transferDTO.NO = "LMC" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + Guid.NewGuid().ToString().Substring(0, 5);

            await db.Transfer.AddAsync(new Transfer
            {
                Distance = transferDTO.Distance,
                FromPlace = transferDTO.FromPlace,
                FromPlaceID = transferDTO.FromPlaceID,
                InsertDate = transferDTO.InsertDate,
                KMPrice = vehicleCategory.KMPrice,
                TotalPrice = vehicleCategory.KMPrice * transferDTO.Distance,
                Name = transferDTO.Name,
                Tel = transferDTO.Tel,
                Message = transferDTO.Message,
                FlieghtNo = transferDTO.FlieghtNo,
                ToPlace = transferDTO.ToPlace,
                ToPlaceID = transferDTO.ToPlaceID,
                VehicleCategoryID = transferDTO.VehicleCategoryID,
                NO = transferDTO.NO,
                Date = transferDTO.Date,
                RoundTripDate = transferDTO.RoundTripDate,
            });

            await db.SaveChangesAsync();

            return View(transferDTO);
        }
    }
}
