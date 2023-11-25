namespace IstanbulLMC.DTOs
{
    public class TransferDTO
    {
        public int VehicleCategoryID { get; set; }

        public string? FromPlaceID { get; set; }

        public string? ToPlaceID { get; set; }

        public decimal? KMPrice { get; set; }

        public decimal? TotalPrice { get; set; }

        public decimal? Distance { get; set; }
    }
}
