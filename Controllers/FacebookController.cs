using Microsoft.AspNetCore.Mvc;
using Reunite.DTOs;
using Reunite.Services.Interfaces;

namespace Reunite.Controllers;

[Route("api/facebook")]
[ApiController]
public class FacebookController : ControllerBase
{
    private readonly IFacebookService facebookService;

    public FacebookController(IFacebookService facebookService)
    {
        this.facebookService = facebookService;
    }

    [HttpPost("post")]
    public async Task<IActionResult> PostToFacebook([FromForm] FinderSearchDTO facebookPostDto)
    {
       string post= await facebookService.FinderPostToFacebook(facebookPostDto);

        return Ok($"facebook is posted and its id is {post}");
    }
}