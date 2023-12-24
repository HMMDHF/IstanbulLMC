namespace IstanbulLMC.Models
{
    public class Service
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<TransferService> TransferServices { get; set; } = new List<TransferService>();
    }
}
