using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Reunite.Controllers
{
    [Route("api/childs")]
    [ApiController]
    public class ChildController : ControllerBase
    {

        private readonly HttpClient httpClient;

        public ChildController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Find(IFormFile image, bool fromParent)
        {
            if (image is null || image.Length == 0)
                return BadRequest("Image is required.");

            using var content = new MultipartFormDataContent();

            var imageContent = new StreamContent(image.OpenReadStream());
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(image.ContentType); // send type of the image [ png, ... ].
            content.Add(imageContent, "file", image.FileName);

            content.Add(new StringContent(fromParent.ToString()), "fromParent");

            var link = "http://127.0.0.1:8000/users/search";
            var response = await httpClient.PostAsync(link, content);

            if (!response.IsSuccessStatusCode)
                return BadRequest(response.ToString());

            var zipFile = await response.Content.ReadAsByteArrayAsync();

            return File(zipFile, "application/zip", "result.zip");
        }

    }
}