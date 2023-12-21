using IstanbulLMC.Models;

namespace IstanbulLMC.DTOs
{
    public class TransferDTO : Transfer
    {
        public DateTime Date { get; set; }
        public DateTime? RoundTripDate { get; set; }
        public int PassengersCount { get; set; }
        public string Duration { get; set; }
        public string NO { get; set; }
        public List<VehicleCategory> VehicleCategories { get; set; } = new List<VehicleCategory>();
    }
}
 