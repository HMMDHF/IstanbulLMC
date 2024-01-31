using IstanbulLMC.Models;

namespace IstanbulLMC.DTOs
{
    public class VehicleCategoryDTO : VehicleCategory
    {
        public decimal Price { get; set; }
        public decimal Distance { get; set; }
        public string From { get; set; }
        public string FromID { get; set; }
        public string To { get; set; }
        public string ToID { get; set; }
        public string Duration { get; set; }
        public int PassengersCount { get; set; }
        public DateTime Date { get; set; }
        public DateTime? RoundTripDate { get; set; }

    }
}
