using IstanbulLMC.Models;
using System.ComponentModel.DataAnnotations;

namespace IstanbulLMC.DTOs
{
    public class TransferDTO : Transfer
    {
        [Required(ErrorMessage = " ")]
        public int PassengersCount { get; set; }

        public string Duration { get; set; }

        public string GoogleCaptchToken { get; set; }

        public string NO { get; set; }

        public string Vehicle { get; set; }

        public virtual List<VehicleCategoryDTO> VehicleCategories { get; set; } = new List<VehicleCategoryDTO>();

        public virtual List<ServiceDTO> Services { get; set; } = new List<ServiceDTO>();
    }
}
 