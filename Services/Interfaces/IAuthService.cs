using Reunite.DTOs.AuthDTOs;

namespace Reunite.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDTO registerDTO);
        Task UpdateAsync(UpdateDTO updateDTO);
        Task UpdatePasswordAsync(UpdatePasswordDTO updatePasswordDTO);
    }
}