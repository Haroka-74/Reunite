using Reunite.DTOs.AuthDTOs;
using Reunite.Services.Interfaces;

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

        public async Task<string> RegisterAsync(RegisterDTO registerDTO)
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

            if (!response.IsSuccessStatusCode)
                return $"Registration failed: {await response.Content.ReadAsStringAsync()}";

            return $"Registration successful: {await response.Content.ReadAsStringAsync()}";
        }

        public Task<string> LoginAsync(LoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }

    }
}