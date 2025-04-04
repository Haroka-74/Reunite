using System.Text.Json.Serialization;

namespace Reunite.Domain
{
    public class RegisterErrorResponse
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

    }
}