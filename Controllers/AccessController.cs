using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Reunite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {

        [HttpGet("all")]
        public IActionResult Get()
        {
            return Ok("This is a public endpoint.");
        }


        [HttpGet("users")]
        [Authorize()]
        public IActionResult Get1()
        {
            return Ok("This is a protected endpoint for any authenticated user.");
        }

        [HttpGet("admins")]
        [Authorize(Roles = "Admin")] 
        public IActionResult Get3()
        {
            return Ok("This is a protected endpoint for users with the 'User' role.");
        }

    }
}
