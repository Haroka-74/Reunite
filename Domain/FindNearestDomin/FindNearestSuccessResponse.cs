using System.Text.Json.Serialization;

namespace Reunite.Domain
{
    public class FindNearestSuccessResponse
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        [JsonPropertyName("isParent")]
        public bool IsParent { get; set; }
        [JsonPropertyName("date")]
        public string Date { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; }
    }
}
