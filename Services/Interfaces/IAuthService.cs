using Reunite.DTOs.AuthDTOs;
using Reunite.Models.Auth;

namespace Reunite.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterDTO registerDTO);
        Task<AuthModel> ForgetPasswordAsync(ForgetPasswordDTO forgetPasswordDTO);
    }
}