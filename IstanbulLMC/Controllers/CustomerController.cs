using IstanbulLMC.DTOs;
using IstanbulLMC.MicroServices;
using IstanbulLMC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Syncfusion.EJ2.Notifications;
using System;

namespace IstanbulLMC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly lmcTourismContext db;
        private readonly SessionService _sessionService;

        public CustomerController(lmcTourismContext context, IHttpContextAccessor httpContextAccessor)
        {
            db = context;
            _sessionService = new SessionService(httpContextAccessor);
        }

        public IActionResult CustomerApplication()
        {
            TransferDTO transferDTO = _sessionService.GetTransferDTO();
            if (transferDTO != null)
            {
                return View(transferDTO);
            }
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> CustomerApplication(TransferDTO transferDTO)
        {
            //if (await RecaptchaService.IsCapchaValid(transferDTO.GoogleCaptchToken, this.HttpContext.Connection.RemoteIpAddress.ToString()))
            //{
                TransferDTO sessiontransferDTO = _sessionService.GetTransferDTO();

                if (sessiontransferDTO == null)
                {
                    return Redirect("/");
                }

                VehicleCategory vehicleCategory = await db.VehicleCategory.FirstOrDefaultAsync(x => x.ID == transferDTO.VehicleCategoryID) ?? new VehicleCategory();

                transferDTO.Vehicle = vehicleCategory.Name;
                transferDTO.TotalPrice = (vehicleCategory.KMPrice * transferDTO.Distance);

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
                _sessionService.SetTransferSession(transferDTO);
                return View(transferDTO);
            //}
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> CustomerApplicationSave(TransferDTO transferDTO)
        {
            //if (await RecaptchaService.IsCapchaValid(transferDTO.GoogleCaptchToken, this.HttpContext.Connection.RemoteIpAddress.ToString()))
            //{
                TransferDTO sessiontransferDTO = _sessionService.GetTransferDTO();

                transferDTO.NO = "LMC" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + Guid.NewGuid().ToString().Substring(0, 5);

                Transfer transfer = new Transfer
                {
                    Distance = sessiontransferDTO.Distance,
                    FromPlace = sessiontransferDTO.FromPlace,
                    FromPlaceID = sessiontransferDTO.FromPlaceID,
                    InsertDate = sessiontransferDTO.InsertDate,
                    KMPrice = sessiontransferDTO.KMPrice,
                    TotalPrice = sessiontransferDTO.KMPrice * transferDTO.Distance,
                    Name = transferDTO.Name,
                    Tel = transferDTO.Tel,
                    Message = transferDTO.Message,
                    FlieghtNo = transferDTO.FlieghtNo,
                    ToPlace = sessiontransferDTO.ToPlace,
                    ToPlaceID = sessiontransferDTO.ToPlaceID,
                    VehicleCategoryID = sessiontransferDTO.VehicleCategoryID,
                    NO = transferDTO.NO,
                    Date = sessiontransferDTO.Date,
                    RoundTripDate = sessiontransferDTO.RoundTripDate,
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


                sessiontransferDTO.Name = transferDTO.Name;
                sessiontransferDTO.Tel = transferDTO.Tel;
                sessiontransferDTO.Message = transferDTO.Message;
                sessiontransferDTO.FlieghtNo = transferDTO.FlieghtNo;
                sessiontransferDTO.NO = transferDTO.NO;

                _sessionService.SetTransferSession(sessiontransferDTO);


                return View(transferDTO);
            //}
            return Redirect("/");
        }
    }
}
