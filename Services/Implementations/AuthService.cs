using System.Net.Http.Headers;
using Reunite.DTOs.AuthDTOs;
using Reunite.Models;
using Reunite.Repositories.Interfaces;
using Reunite.Services.Interfaces;

namespace Reunite.Services.Implementations
{
    public class AuthService(HttpClient httpClient, IUserRepository userRepository, ITokenProviderService tokenProviderService, IConfiguration configuration) : IAuthService
    {

    public async Task RegisterAsync(RegisterDTO registerDTO)
        {
            await userRepository.AddUserAsync(new ReuniteUser
            {
                Id = registerDTO.Auth0Id,
                Username = registerDTO.Username,
                Email = registerDTO.Email
            });
        }

        public async Task UpdateAsync(UpdateDTO updateDTO)
        {
            string token = await tokenProviderService.GetTokenAsync();

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

            await userRepository.UpdateUserAsync(updateDTO.Id, new ReuniteUser
            {
                Username = updateDTO.Name,
                Email = updateDTO.Email
            });

            var response = await httpClient.PatchAsJsonAsync($"https://{configuration["ManAuth0API:Domain"]}/api/v2/users/{updateDTO.Id}", payload);
            response.EnsureSuccessStatusCode();
        }

    }
}