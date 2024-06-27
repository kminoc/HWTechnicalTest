using System.Text.Json.Serialization;

namespace HWTechnicalTest.Model
{
    public class OfferOrigin
    {
        [JsonPropertyName("urlOrigine")]
        public string UrlOrigine { get; set; }
    }
}
