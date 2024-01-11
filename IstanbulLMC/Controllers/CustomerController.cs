using IstanbulLMC.DTOs;
using IstanbulLMC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;

namespace IstanbulLMC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly lmcTourismContext db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerController(lmcTourismContext context, IHttpContextAccessor httpContextAccessor)
        {
            db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult CustomerApplication()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            string transferDTO = session.GetString("transferDTO");

            if (!string.IsNullOrEmpty(transferDTO))
            {
                return View(JsonConvert.DeserializeObject<TransferDTO>(transferDTO));
            }

            return Redirect("/");
        }   

        [HttpPost]
        public async Task<IActionResult> CustomerApplication(TransferDTO transferDTO)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            string transferStr = session.GetString("transferDTO");
            
            VehicleCategory vehicleCategory = await db.VehicleCategory.FirstOrDefaultAsync(x => x.ID == transferDTO.VehicleCategoryID) ?? new VehicleCategory();

            transferDTO.Vehicle = vehicleCategory.Name;
            transferDTO.TotalPrice = (vehicleCategory.KMPrice * transferDTO.Distance);

            if (string.IsNullOrEmpty(transferStr))
            {
                return Redirect("/");
            }

            TransferDTO sessiontransferDTO = JsonConvert.DeserializeObject<TransferDTO>(transferStr);

            transferDTO.Distance = sessiontransferDTO.Distance;
            transferDTO.FromPlace = sessiontransferDTO.FromPlace;
            transferDTO.FromPlaceID = sessiontransferDTO.FromPlaceID;
            transferDTO.ToPlace = sessiontransferDTO.ToPlace;
            transferDTO.ToPlaceID = sessiontransferDTO.ToPlaceID;
            transferDTO.PassengersCount = sessiontransferDTO.PassengersCount;
            transferDTO.Date = sessiontransferDTO.Date;
            transferDTO.RoundTripDate = sessiontransferDTO.RoundTripDate;
            transferDTO.Duration = sessiontransferDTO.Duration;
            transferDTO.VehicleCategoryID = sessiontransferDTO.VehicleCategoryID;

            transferDTO.Services = await db.Service.Where(x => x.IsActive).Select(x => new ServiceDTO
            {
                Name = x.Name,
                ID = x.ID,
                IsActive = x.IsActive,
                Price = x.Price,
                Icon = x.Icon,
            }).ToListAsync();

            session.SetString("transferDTO", JsonConvert.SerializeObject(transferDTO));
            return View(transferDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CustomerApplicationSave(TransferDTO transferDTO)
        {
            VehicleCategory vehicleCategory = await db.VehicleCategory.FirstOrDefaultAsync(x => x.ID == transferDTO.VehicleCategoryID) ?? new VehicleCategory();

            transferDTO.NO = "LMC" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + Guid.NewGuid().ToString().Substring(0, 5);

            Transfer transfer = new Transfer
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
            };

            await db.Transfer.AddAsync(transfer);

            await db.SaveChangesAsync();

            foreach (var service in transferDTO.Services)
            {
                if (service.IsSelected)
                {
                    await db.TransferService.AddAsync(
                        new TransferService
                        {
                            ServiceID = service.ID,
                            TransferID = transfer.ID,
                            Price = service.Price,
                        }
                    );
                }
            }

            await db.SaveChangesAsync();

            return View(transferDTO);
        }
    }
}
