using Microsoft.AspNetCore.Mvc;
using Reunite.DTOs;
using Reunite.Services.Interfaces;

namespace Reunite.Controllers
{
    [Route("api/childs")]
    [ApiController]
    public class ChildController : ControllerBase
    {

        private readonly IChildService childService;

        public ChildController(IChildService childService)
        {
            this.childService = childService;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromForm]SearchDTO searchDto)
        {
            var response = await childService.FindNearest(searchDto);
            if (response.Ok) return Ok(response.Success);
            await childService.AddChild(searchDto);
            return NotFound(response.Error);
        }
        
    }
}