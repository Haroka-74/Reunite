using Microsoft.AspNetCore.Mvc;
using Reunite.DTOs.AuthDTOs;
using Reunite.Services.Interfaces;

namespace Reunite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService service;

        public AuthController(IAuthService service)
        {
            this.service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await service.RegisterAsync(registerDTO);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await service.LoginAsync(loginDTO);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenDTO refreshTokenDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await service.RefreshTokenAsync(refreshTokenDTO);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke(RefreshTokenDTO refreshTokenDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await service.RevokeTokenAsync(refreshTokenDTO);

            if (!result)
                return BadRequest("Token could not be revoked.");

            return Ok("Token revoked successfully.");
        }

    }
}