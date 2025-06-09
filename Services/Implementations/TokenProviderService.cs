using Reunite.Services.Interfaces;
using System.Text.Json;

namespace Reunite.Services.Implementations
{
    public class TokenProviderService(HttpClient httpClient, IConfiguration configuration) : ITokenProviderService
    {

        private readonly HttpClient httpClient = httpClient;
        private readonly IConfiguration configuration = configuration;

        public async Task<string> GetTokenAsync()
        {
            var payload = new
            {
                client_id = configuration["ManAuth0API:ClientId"],
                client_secret = configuration["ManAuth0API:ClientSecret"],
                audience = configuration["ManAuth0API:Audience"],
                grant_type = "client_credentials"
            };

            var response = await httpClient.PostAsJsonAsync($"https://{configuration["ManAuth0API:Domain"]}/oauth/token", payload);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            return json.GetProperty("access_token").GetString()!;
        }

    }
}