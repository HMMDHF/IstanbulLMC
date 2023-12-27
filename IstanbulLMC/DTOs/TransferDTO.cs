using IstanbulLMC.Models;

namespace IstanbulLMC.DTOs
{
    public class TransferDTO : Transfer
    {
        public int PassengersCount { get; set; }
        public string Duration { get; set; }
        public string NO { get; set; }
        public virtual List<VehicleCategoryDTO> VehicleCategories { get; set; } = new List<VehicleCategoryDTO>();
        public virtual List<ServiceDTO> Services { get; set; } = new List<ServiceDTO>();
    }
}
 