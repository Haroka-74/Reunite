using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reunite.DTOs;
using Reunite.Services.Interfaces;

namespace Reunite.Controllers
{
    [Route("api/childs")]
    [ApiController]
    [Authorize]
    public class ChildController : ControllerBase
    {

        private readonly IChildService childService;
        private readonly IFacebookService facebookService;

        public ChildController(IChildService childService,IFacebookService facebookService)
        {
            this.childService = childService;
            this.facebookService = facebookService;
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
            string post = await facebookService.ParentPostToFacebook(searchDto);
            var parts = post.Split('_');
            string pageId = parts[0];
            string postId = parts[1];
            string postUrl= $"https://www.facebook.com/{pageId}/posts/{postId}";

            return StatusCode(201, new { Message = "Child added successfully, wait to antoher person find you child please.",  postUrl});
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
            string post = await facebookService.FinderPostToFacebook(searchDto);
            var parts = post.Split('_');
            string pageId = parts[0];
            string postId = parts[1];
            string postUrl= $"https://www.facebook.com/{pageId}/posts/{postId}";


            return StatusCode(201, new { Message = "Child added successfully, wait to antoher person find you child please.", postUrl });
        }

    }
}