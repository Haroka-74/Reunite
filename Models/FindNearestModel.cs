using System.Text.Json.Serialization;

namespace Reunite.Models
{
    public class FindNearestModel
    {
        [JsonPropertyName("image")]
        public string? Image { get; set; }

        [JsonPropertyName("_id")]
        public string? Id { get; set; }

        [JsonPropertyName("isParent")]
        public bool IsParent { get; set; }

        [JsonPropertyName("date")]
        public string? Date { get; set; }

        [JsonIgnore]
        public int StautsCode { get; set; }
    }
}
