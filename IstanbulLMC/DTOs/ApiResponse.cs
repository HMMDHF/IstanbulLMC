using Newtonsoft.Json;

namespace IstanbulLMC.DTOs
{
    public class ApiResponse
    {
        [JsonProperty("results")]
        public List<SubApiResponse> subApiResponses { get; set; }
    }

    public class SubApiResponse
    {
        [JsonProperty("place_id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("formatted_address")]
        public string Address { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
        public List<double> coordinates { get; set; }
    }
}
