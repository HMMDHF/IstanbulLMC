namespace IstanbulLMC.Areas.Admin.DTOs
{
    public class SyncfusionGridDTO<T>
    {
        public string action { get; set; }
        public int key { get; set; }
        public string keyColumn { get; set; }
        public T value { get; set; }
    }
}