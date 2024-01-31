using Newtonsoft.Json;

namespace IstanbulLMC.DTOs
{
    public class YandexApiResponse
    {
        public string type { get; set; }
        public Properties properties { get; set; }
        public List<Feature> features { get; set; }
    }

    public class Feature
    {
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
    }

    public class GeocoderMetaData
    {
        public string precision { get; set; }
        public string text { get; set; }
        public string kind { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class Properties
    {
        public ResponseMetaData ResponseMetaData { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<List<double>> boundedBy { get; set; }
        public string uri { get; set; }
        public GeocoderMetaData GeocoderMetaData { get; set; }
    }

    public class ResponseMetaData
    {
        public SearchResponse SearchResponse { get; set; }
        public SearchRequest SearchRequest { get; set; }
    }

    public class SearchRequest
    {
        public string request { get; set; }
        public int skip { get; set; }
        public int results { get; set; }
        public List<List<double>> boundedBy { get; set; }
    }

    public class SearchResponse
    {
        public int found { get; set; }
        public string display { get; set; }
        public List<List<double>> boundedBy { get; set; }
    }
}
