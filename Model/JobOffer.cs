using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace HWTechnicalTest.Model
{
    public class JobOffer
    {
        [BsonId]
        [JsonPropertyName("id")]
        [BsonRepresentation(BsonType.String)]
        public string? Id { get; set; }
        [JsonPropertyName("intitule")]
        public string? Intitule { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("dateCreation")]
        public DateTime? DateCreation { get; set; }
        [JsonPropertyName("typeContratLibelle")]
        public string? TypeContratLibelle { get; set; }
        [JsonPropertyName("experienceLibelle")]
        public string? ExperienceLibelle { get; set; }
        [JsonPropertyName("entreprise")]
        public Company? Entreprise { get; set; }
        [JsonPropertyName("origineOffre")]
        public OfferOrigin? OrigineOffre { get; set; }
        [JsonPropertyName("lieuTravail")]
        public OfferLocation? LieuTravail { get; set; }
        [JsonPropertyName("paysContinent")]
        public string PaysContinent { get; set; }
    }
}
