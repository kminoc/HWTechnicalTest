using System.Text.Json.Serialization;
using HWTechnicalTest.Model;

namespace HWTechnicalTest.FTApi
{
    public class FTApiSearchResponse
    {
        [JsonPropertyName("resultats")]
        public List<JobOffer> Resultats { get; set; }
    }
}
