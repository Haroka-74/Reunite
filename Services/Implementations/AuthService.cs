using Reunite.Domain;
using Reunite.DTOs.AuthDTOs;
using Reunite.Models.Auth;
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

        public async Task<AuthModel> RegisterAsync(RegisterDTO registerDTO)
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

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var registerErrorResponse = JsonSerializer.Deserialize<RegisterErrorResponse>(content);
                return new AuthModel
                {
                    Email = registerDTO.Email,
                    IsAuthenticated = false,
                    Message = registerErrorResponse.Code,
                    StatusCode = registerErrorResponse.StatusCode,
                };
            }

            return new AuthModel
            {
                Email = registerDTO.Email,
                IsAuthenticated = true,
                Message = "User registered successfully.",
                StatusCode = (int)response.StatusCode,
            };
        }

        public async Task<AuthModel> ForgetPasswordAsync(ForgetPasswordDTO forgetPasswordDTO)
        {
            var payload = new
            {
                client_id = configuration["Auth0:ClientId"],
                connection = configuration["Auth0:Connection"],
                email = forgetPasswordDTO.Email,
            };
            string URL = $"https://{configuration["Auth0:Domain"]}/dbconnections/change_password";

            var response = await httpClient.PostAsJsonAsync(URL, payload);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new AuthModel
                {
                    Email = forgetPasswordDTO.Email,
                    IsAuthenticated = false,
                    Message = "Email is required.",
                    StatusCode = (int)response.StatusCode,
                };
            }
            return new AuthModel
            {
                Email = forgetPasswordDTO.Email,
                IsAuthenticated = true,
                Message = content,
                StatusCode = (int)response.StatusCode,
            };
        }
    }
}