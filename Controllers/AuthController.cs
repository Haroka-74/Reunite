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
        
        [HttpPost("forget_password")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDTO forgetPasswordDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await service.ForgetPasswordAsync(forgetPasswordDTO);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
        

    }
}