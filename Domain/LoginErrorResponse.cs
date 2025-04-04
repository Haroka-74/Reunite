using System.Text.Json.Serialization;

namespace Reunite.Domain
{
    public class LoginErrorResponse
    {

        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("error_description")]
        public string ErrorDescription { get; set; }

    }
}