using System.Net.Http.Headers;
using System.Text.Json;
using Reunite.DTOs.AuthDTOs;
using Reunite.Models;
using Reunite.Repositories.Interfaces;
using Reunite.Services.Interfaces;

namespace Reunite.Services.Implementations
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository repository;
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public AuthService(IUserRepository repository, HttpClient httpClient, IConfiguration configuration)
        {
            this.repository = repository;
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task RegisterAsync(RegisterDTO registerDTO)
        {
            await repository.AddUser(new ReuniteUser
            { Id = registerDTO.Auth0Id, Username = registerDTO.Username, Email = registerDTO.Email });
        }

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

        public async Task UpdateAsync(UpdateDTO updateDTO)
        {
            string token = await GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var payload = new
            {
                name = updateDTO.Name,
                email = updateDTO.Email,
                user_metadata = new
                {
                    phone_number = updateDTO.PhoneNumber
                }
            };

            await repository.UpdateUser(updateDTO.Id, new ReuniteUser
            {
                Username = updateDTO.Name,
                Email = updateDTO.Email
            });

            var response = await httpClient.PatchAsJsonAsync($"https://{configuration["ManAuth0API:Domain"]}/api/v2/users/{updateDTO.Id}", payload);
            response.EnsureSuccessStatusCode();
        }

    }
}