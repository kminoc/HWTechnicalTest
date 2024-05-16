using System.Text.Json.Serialization;

namespace HWTechnicalTest
{
    public class Company
    {
        [JsonPropertyName("nom")]
        public string Nom { get; set; }
    }
}
