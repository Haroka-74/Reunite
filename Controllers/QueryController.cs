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
    [Authorize]
    public class QueryController : ControllerBase
    {
        private readonly IQueryService queryService;
        private readonly IFacebookService facebookService;

        public QueryController(IQueryService queryService, IFacebookService facebookService)
        {
            this.queryService = queryService;
            this.facebookService = facebookService;
        }

        [HttpPost("p/search")]
        public async Task<IActionResult> ParentSearch([FromForm] ParentSearchDTO searchDto)
        {
            var result = await queryService.FindNearest(searchDto, true);
            QueryDTO query = await queryService.AddQueryByParent(searchDto);

            if (result.IsSuccess)
                return StatusCode(result.StatusCode, result.Data);


            string postUrl = await facebookService.ParentPostToFacebook(searchDto,query.Id);
            return StatusCode(result.StatusCode, new { error = result.Error, postUrl });
        }

        [HttpPost("f/search")]
        public async Task<IActionResult> FinderSearch([FromForm] FinderSearchDTO searchDto)
        {
            var result = await queryService.FindNearest(searchDto, false);
            QueryDTO query =await queryService.AddQueryByFinder(searchDto);
            if (result.IsSuccess)
                return StatusCode(result.StatusCode, result.Data);

            string postUrl = await facebookService.FinderPostToFacebook(searchDto,query.Id);
            return StatusCode(result.StatusCode, new { error = result.Error, postUrl });
        }
    }
}