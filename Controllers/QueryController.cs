using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reunite.DTOs;
using Reunite.DTOs.QueryDTOs;
using Reunite.Repositories.Interfaces;
using Reunite.Services.Interfaces;
using Reunite.Shared;

namespace Reunite.Controllers
{
    [Route("api/query")]
    [ApiController]
  //  [Authorize]
    public class QueryController(IQueryService queryService, IFacebookService facebookService) : ControllerBase
    {
        [HttpPost("parent")]
        public async Task<IActionResult> ParentSearch([FromForm] ParentSearchDTO searchDto)
        {
            var result = await queryService.FindNearest(searchDto, true);
            QueryDTO query = await queryService.AddQueryByParent(searchDto);

            if (result.IsSuccess)
                return StatusCode(result.StatusCode, result.Data);


            string postUrl = await facebookService.ParentPostToFacebook(searchDto, query.Id);
            return StatusCode(result.StatusCode, new { error = result.Error, postUrl });
        }

        [HttpPost("finder")]
        public async Task<IActionResult> FinderSearch([FromForm] FinderSearchDTO searchDto)
        {
            var result = await queryService.FindNearest(searchDto, false);
            QueryDTO query = await queryService.AddQueryByFinder(searchDto);
            if (result.IsSuccess)
                return StatusCode(result.StatusCode, result.Data);

            string postUrl = await facebookService.FinderPostToFacebook(searchDto, query.Id);
            return StatusCode(result.StatusCode, new { error = result.Error, postUrl });
        }

        [HttpGet("user-queries/{userID}")]
        public async Task<IActionResult> GetUserQueries(string userID)
        {
            var result = await queryService.GetUserQueriesAsync(userID);
            if (result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Data);
            }

            return StatusCode(result.StatusCode, new { error = result.Error });
        }

        [HttpGet("{queryId}")]
        public async Task<IActionResult> GetQuery(string queryId)
        {
            var result = await queryService.GetQueryAsync(queryId);
            if (result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Data);
            }

            return StatusCode(result.StatusCode, new { error = result.Error });
        }
        
        [HttpPut("{queryId}")]
        public async Task<IActionResult> CompleteQuery(string queryId)
        {
            var result = await queryService.ChangeIsCompleted(queryId);
            if (result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Data);
            }

            return StatusCode(result.StatusCode, new { error = result.Error });
        }
    }
}