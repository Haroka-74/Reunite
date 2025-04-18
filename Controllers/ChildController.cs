using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reunite.DTOs;
using Reunite.Services.Interfaces;

namespace Reunite.Controllers
{
    [Route("api/childs")]
    [ApiController]
    //[Authorize]
    public class ChildController : ControllerBase
    {

        private readonly IChildService childService;

        public ChildController(IChildService childService)
        {
            this.childService = childService;
        }

        [HttpPost("p/search")]
        public async Task<IActionResult> ParentSearch([FromForm] ParentSearchDTO searchDto)
        {
            var response = await childService.FindNearest(searchDto);

            if (response.Ok)
            {
                return Ok(response.Success);
            }

            if (response.Error.StatusCode == 400) return BadRequest(response.Error);

            await childService.AddChildByParent(searchDto);

            return StatusCode(201, new { Message = "Child added successfully, wait to antoher person find you child please." });
        }

        [HttpPost("f/search")]
        public async Task<IActionResult> FinderSearch([FromForm] FinderSearchDTO searchDto)
        {
            var response = await childService.FindNearest(searchDto);

            if (response.Ok)
            {
                return Ok(response.Success);
            }

            if (response.Error.StatusCode == 400) return BadRequest(response.Error);

            await childService.AddChildByFinder(searchDto);

            return StatusCode(201, new { Message = "Thanks for adding the child." });
        }

    }
}