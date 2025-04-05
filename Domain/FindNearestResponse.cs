using System.Text.Json.Serialization;

namespace Reunite.Domain
{
    public class FindNearestResponse
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        [JsonPropertyName("fromParent")]
        public string FromParent { get; set; }
        [JsonPropertyName("date")]
        public string Date { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; }
    }
}
