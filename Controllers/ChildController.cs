using Microsoft.AspNetCore.Mvc;
using Reunite.DTOs;
using Reunite.Services.Interfaces;

namespace Reunite.Controllers
{
    [Route("api/childs")]
    [ApiController]
    public class ChildController : ControllerBase
    {

        private readonly IChildServies childServies;

        public ChildController(IChildServies childServies)
        {
            this.childServies = childServies;
        }

        [HttpPost("search")]
        public async Task<IActionResult> FindNearest(IFormFile image, bool isParent)
        {
            ChildDTO childDTO = new ChildDTO
            {
                Image = image,
                IsParent = isParent
            };

            var response = await childServies.FindNearest(childDTO);

            if (response.StautsCode == 200) return Ok(response);
            return StatusCode(404, response);
        }

    }
}