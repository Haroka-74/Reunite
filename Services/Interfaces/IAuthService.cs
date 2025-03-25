using Reunite.DTOs.AuthDTOs;
using Reunite.Models.Auth;

namespace Reunite.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterDTO registerDTO);
        Task<AuthModel> LoginAsync(LoginDTO loginDTO);
        Task<AuthModel> RefreshTokenAsync(RefreshTokenDTO refreshTokenDTO);
        Task<bool> RevokeTokenAsync(RefreshTokenDTO refreshTokenDTO);
    }
}