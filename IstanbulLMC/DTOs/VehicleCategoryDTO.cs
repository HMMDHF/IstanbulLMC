using IstanbulLMC.Models;

namespace IstanbulLMC.DTOs
{
    public class VehicleCategoryDTO
    {
        public decimal Price { get; set; }
        public decimal Distance { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Duration { get; set; }
        public List<VehicleCategory> VehicleCategories { get; set; } = new List<VehicleCategory>();
    }
}
