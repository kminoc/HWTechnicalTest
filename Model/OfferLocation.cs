using System.Text.Json.Serialization;

namespace HWTechnicalTest.Model
{
    public class OfferLocation
    {
        [JsonPropertyName("libelle")]
        public string Libelle { get; set; }
        [JsonPropertyName("codePostal")]
        public string CodePostal { get; set; }
        [JsonPropertyName("commune")]
        public string Commune { get; set; }
    }
}
