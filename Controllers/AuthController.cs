using Microsoft.AspNetCore.Mvc;
using Reunite.DTOs.AuthDTOs;
using Reunite.Services.Interfaces;

namespace Reunite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase
    {

        private readonly IAuthService service = service;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            await service.RegisterAsync(registerDTO);
            return Ok(new { Message = "User added successfully" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateDTO updateDTO)
        {
            await service.UpdateAsync(updateDTO);
            return Ok(new { Message = "User updated successfully" });
        }

        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDTO updatePasswordDTO)
        {
            await service.UpdatePasswordAsync(updatePasswordDTO);
            return Ok(new { Message = "Password updated successfully" });
        }

    }
}