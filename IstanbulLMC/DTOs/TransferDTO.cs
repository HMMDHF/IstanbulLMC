using IstanbulLMC.Models;

namespace IstanbulLMC.DTOs
{
    public class TransferDTO : Transfer
    {
        public DateTime Date { get; set; }
        public DateTime? RounTripDate { get; set; }
        public int PassengersCount { get; set; }
    }
}
