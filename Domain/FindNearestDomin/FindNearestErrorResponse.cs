using System.Text.Json.Serialization;

namespace Reunite.Domain
{
    public class FindNearestErrorResponse
    {
        [JsonPropertyName("detail")]
        public Detail Detail { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }

    }
    public class Detail
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

}