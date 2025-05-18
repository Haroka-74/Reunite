using Reunite.DTOs.AuthDTOs;

namespace Reunite.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDTO registerDTO);
        Task<string> GetTokenAsync();
        Task UpdateAsync(UpdateDTO updateDTO);
    }
}