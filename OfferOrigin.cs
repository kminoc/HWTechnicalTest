using System.Text.Json.Serialization;

namespace HWTechnicalTest
{
    public class OfferOrigin
    {
        [JsonPropertyName("urlOrigine")]
        public string UrlOrigine { get; set; }
    }
}
