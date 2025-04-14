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

            if (response.Ok) return Ok(response.Success);

            if (response.Error!.Detail.Name == "No similar images")
            {
                await childService.AddChildByParent(searchDto);
                return Ok(new { Message = "Child added successfully" });
            }
            return NotFound(response.Error);
        }

        [HttpPost("f/search")]
        public async Task<IActionResult> FinderSearch([FromForm] FinderSearchDTO searchDto)
        {
            var response = await childService.FindNearest(searchDto);
            if (response.Ok) return Ok(response.Success);
            if (response.Error!.Detail.Name == "No similar images")
            {
                await childService.AddChildByFinder(searchDto);
                return Ok(new { Message = "Child added successfully" });
            }
            return NotFound(response.Error);
        }

    }
}