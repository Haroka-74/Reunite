using System.Net.Http.Headers;
using System.Text.Json;
using Reunite.Domain;
using Reunite.DTOs;
using Reunite.Models;
using Reunite.Services.Interfaces;

namespace Reunite.Services.Implementations
{
    public class ChildService : IChildServies
    {
        private readonly HttpClient httpClient;

        public ChildService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<FindNearestModel> FindNearest(ChildDTO childDTO)
        {

            using var content = new MultipartFormDataContent();

            var imageContent = new StreamContent(childDTO.Image.OpenReadStream());
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(childDTO.Image.ContentType); // send type of the image [ png, ... ].
            content.Add(imageContent, "image", childDTO.Image.FileName);

            content.Add(new StringContent(childDTO.FromParent.ToString()), "fromParent");

            var url = "http://159.65.54.71:54621";
            var link = $"{url}/childs/search";
            var response = await httpClient.PostAsync(link, content);
            var responseStringContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var ErrorResponse = JsonSerializer.Deserialize<FindNearestErrorResponse>(responseStringContent);
                return new FindNearestModel
                {
                    Message = ErrorResponse.detail.Message,
                    StautsCode = (int)response.StatusCode
                };
            }
            var findNearestResponse = JsonSerializer.Deserialize<FindNearestResponse>(responseStringContent);

            return new FindNearestModel
            {
                Image = findNearestResponse.Image,
                Id = findNearestResponse.Id,
                FromParent = findNearestResponse.FromParent,
                Message = "Sucssess",
                StautsCode = (int)response.StatusCode,
                Date = findNearestResponse.Date
            };

        }


    }
}
