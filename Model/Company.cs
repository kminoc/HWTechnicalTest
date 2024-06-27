using System.Text.Json.Serialization;

namespace HWTechnicalTest.Model
{
    public class Company
    {
        [JsonPropertyName("nom")]
        public string Nom { get; set; }
    }
}
