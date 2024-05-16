using System.Text.Json.Serialization;

namespace HWTechnicalTest.FTApi
{
    public class FTApiSearchResponse
    {
        [JsonPropertyName("resultats")]
        public List<JobOffer> Resultats { get; set; }
    }
}
