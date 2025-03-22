using Reunite.Domain;
using Reunite.DTOs.AuthDTOs;

namespace Reunite.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDTO registerDTO);
        Task<LoginResponse> LoginAsync(LoginDTO loginDTO);
    }
}