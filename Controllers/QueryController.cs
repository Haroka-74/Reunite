using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reunite.DTOs;
using Reunite.DTOs.QueryDTOs;
using Reunite.Services.Interfaces;
using Reunite.Shared;

namespace Reunite.Controllers
{
    [Route("api/query")]
    [ApiController]
    //[Authorize]
    public class QueryController : ControllerBase
    {











        private readonly IQueryService queryService;
        //private readonly IFacebookService facebookService;

        public QueryController(IQueryService childService/* , IFacebookService facebookService */)
        {
            this.queryService = childService;
            //this.facebookService = facebookService;
        }

        [HttpPost("p/search")]
        public async Task<IActionResult> ParentSearch([FromForm] ParentSearchDTO searchDto)
        {
            var result = await queryService.FindNearest(searchDto);

            if (result.IsSuccess)
                return Ok(result.Data);







            //await queryService.AddChildByParent(searchDto);
            //string post = await facebookService.ParentPostToFacebook(searchDto);
            //var parts = post.Split('_');
            //string pageId = parts[0];
            //string postId = parts[1];
            //string postUrl = $"https://www.facebook.com/{pageId}/posts/{postId}";

            return StatusCode(201, new
            {
                Message = "Child added successfully, wait to antoher person find you child please.",
                //postUrl
            });
        }

        [HttpPost("f/search")]
        public async Task<IActionResult> FinderSearch([FromForm] FinderSearchDTO searchDto)
        {

            var result = await queryService.FindNearest(searchDto);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, new { error = result.Error });



            //await queryService.AddChildByFinder(searchDto);
            //string post = await facebookService.FinderPostToFacebook(searchDto);
            //var parts = post.Split('_');
            //string pageId = parts[0];
            //string postId = parts[1];
            //string postUrl = $"https://www.facebook.com/{pageId}/posts/{postId}";


            return StatusCode(201, new
            {
                Message = "Child added successfully, wait to antoher person find you child please.",
                //postUrl
            });
        }

    }
}