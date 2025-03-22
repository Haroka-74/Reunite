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

            return (result) ? Ok("Registration successful.") : BadRequest("Registration failed.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await service.LoginAsync(loginDTO);

            if (response.AccessToken is null)
                return Unauthorized("Invalid credentials or authentication failed.");

            return Ok(response);
        }

    }
}