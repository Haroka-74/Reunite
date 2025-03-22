using System.Text.Json.Serialization;

namespace Reunite.Domain
{
    public class LoginResponse
    {

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("id_token")]
        public string IdToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresAt { get; set; }

    }
}