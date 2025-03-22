using Microsoft.AspNetCore.Identity.Data;
using Reunite.Domain;
using Reunite.DTOs.AuthDTOs;
using Reunite.Services.Interfaces;
using System.Text.Json;

namespace Reunite.Services.Implementations
{
    public class AuthService : IAuthService
    {

        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public AuthService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<bool> RegisterAsync(RegisterDTO registerDTO)
        {
            var payload = new
            {
                client_id = configuration["Auth0:ClientId"],
                connection = configuration["Auth0:Connection"],
                name = registerDTO.Username,
                email = registerDTO.Email,
                password = registerDTO.Password,
                user_metadata = new
                {
                    phone_number = registerDTO.PhoneNumber
                }
            };

            string URL = $"https://{configuration["Auth0:Domain"]}/dbconnections/signup";

            var response = await httpClient.PostAsJsonAsync(URL, payload);

            return response.IsSuccessStatusCode;
        }

        public async Task<LoginResponse> LoginAsync(LoginDTO loginDTO)
        {
            var payload = new
            {
                client_id = configuration["Auth0:ClientId"],
                connection = configuration["Auth0:Connection"],
                client_secret = configuration["Auth0:ClientSecret"],
                audience = configuration["Auth0:Audience"],
                grant_type = "password",
                username = loginDTO.Username,
                password = loginDTO.Password,
                scope = "openid profile email"
            };
            
            string URL = $"https://{configuration["Auth0:Domain"]}/oauth/token";

            var response = await httpClient.PostAsJsonAsync(URL, payload);

            var content = await response.Content.ReadAsStringAsync();
            
            var json = JsonSerializer.Deserialize<LoginResponse>(content);
            
            return json;
        }

    }
}