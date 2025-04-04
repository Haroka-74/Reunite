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
                    Message = registerErrorResponse.Description,
                    StatusCode = registerErrorResponse.StatusCode,
                    AccessToken = null,
                    AccessTokenExpiration = null,
                    RefreshToken = null
                };
            }

            return new AuthModel
            {
                Email = registerDTO.Email,
                IsAuthenticated = true,
                Message = "User registered successfully.",
                StatusCode = (int) response.StatusCode,
                AccessToken = null,
                AccessTokenExpiration = null,
                RefreshToken = null
            };
        }

        public async Task<AuthModel> LoginAsync(LoginDTO loginDTO)
        {
            var payload = new
            {
                client_id = configuration["Auth0:ClientId"],
                connection = configuration["Auth0:Connection"],
                client_secret = configuration["Auth0:ClientSecret"],
                audience = configuration["Auth0:Audience"],
                grant_type = "password",
                username = loginDTO.Email,
                password = loginDTO.Password,
                scope = configuration["Auth0:LoginScope"]
            };

            string URL = $"https://{configuration["Auth0:Domain"]}/oauth/token";

            var response = await httpClient.PostAsJsonAsync(URL, payload);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var loginErrorResponse = JsonSerializer.Deserialize<LoginErrorResponse>(content);
                return new AuthModel
                {
                    Email = loginDTO.Email,
                    IsAuthenticated = false,
                    Message = loginErrorResponse.ErrorDescription,
                    StatusCode = (int) response.StatusCode,
                    AccessToken = null,
                    AccessTokenExpiration = null,
                    RefreshToken = null
                };
            }

            var loginSuccessResponse = JsonSerializer.Deserialize<LoginSuccessResponse>(content);

            return new AuthModel
            {
                Email = loginDTO.Email,
                IsAuthenticated = true,
                Message = "User logged in successfully.",
                StatusCode = (int) response.StatusCode,
                AccessToken = loginSuccessResponse.AccessToken,
                AccessTokenExpiration = DateTime.UtcNow.AddSeconds(loginSuccessResponse.ExpiresIn),
                RefreshToken = loginSuccessResponse.RefreshToken
            };
        }

        public async Task<AuthModel> RefreshTokenAsync(RefreshTokenDTO refreshTokenDTO)
        {
            var payload = new
            {
                grant_type = "refresh_token",
                client_id = configuration["Auth0:ClientId"],
                client_secret = configuration["Auth0:ClientSecret"],
                refresh_token = refreshTokenDTO.RefreshToken
            };

            string URL = $"https://{configuration["Auth0:Domain"]}/oauth/token";

            var response = await httpClient.PostAsJsonAsync(URL, payload);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var loginErrorResponse = JsonSerializer.Deserialize<LoginErrorResponse>(content);
                return new AuthModel
                {
                    IsAuthenticated = false,
                    Message = loginErrorResponse.ErrorDescription,
                    StatusCode = (int)response.StatusCode,
                    AccessToken = null,
                    AccessTokenExpiration = null,
                    RefreshToken = null
                };
            }

            var loginSuccessResponse = JsonSerializer.Deserialize<LoginSuccessResponse>(content);

            return new AuthModel
            {
                IsAuthenticated = true,
                Message = "Token refreshed successfully.",
                StatusCode = (int) response.StatusCode,
                AccessToken = loginSuccessResponse.AccessToken,
                AccessTokenExpiration = DateTime.UtcNow.AddSeconds(loginSuccessResponse.ExpiresIn),
                RefreshToken = loginSuccessResponse.RefreshToken
            };
        }

        public async Task<bool> RevokeTokenAsync(RefreshTokenDTO refreshTokenDTO)
        {
            var payload = new
            {
                client_id = configuration["Auth0:ClientId"],
                client_secret = configuration["Auth0:ClientSecret"],
                token = refreshTokenDTO.RefreshToken
            };

            string URL = $"https://{configuration["Auth0:Domain"]}/oauth/revoke";

            var response = await httpClient.PostAsJsonAsync(URL, payload);
            
            return response.IsSuccessStatusCode;
        }

    }
}