using IstanbulLMC.DTOs;
using IstanbulLMC.Models;
using Microsoft.AspNetCore.Mvc;
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

        //public CustomerController()
        //{
        //    db = new lmcTourismContext();
        //}

        [HttpPost]
        public IActionResult CustomerApplication(TransferDTO transferDTO)
        {
            return View(transferDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CustomerApplicationSave(TransferDTO transferDTO)
        {

            transferDTO.NO = "LMC" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + Guid.NewGuid().ToString().Substring(0, 5);
            await db.Transfer.AddAsync(new Transfer
            {
                Distance = transferDTO.Distance,
                FromPlace = transferDTO.FromPlace,
                FromPlaceID = transferDTO.FromPlaceID,
                InsertDate = transferDTO.InsertDate,
                KMPrice = transferDTO.KMPrice,
                Name = transferDTO.Name,
                Tel = transferDTO.Tel,
                Message = transferDTO.Message,
                FlieghtNo = transferDTO.FlieghtNo,
                ToPlace = transferDTO.ToPlace,
                ToPlaceID = transferDTO.ToPlaceID,
                VehicleCategoryID = transferDTO.VehicleCategoryID,
                NO = transferDTO.NO,
            });
            await db.SaveChangesAsync();
            return View(transferDTO);
        }
    }
}
