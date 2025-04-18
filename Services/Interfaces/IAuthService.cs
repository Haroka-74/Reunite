using Reunite.DTOs.AuthDTOs;
using Reunite.Models.Auth;

namespace Reunite.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDTO registerDTO);
        Task<string> GetTokenAsync();
        Task UpdateAsync(UpdateDTO updateDTO);
    }
}