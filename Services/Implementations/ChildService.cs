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

            content.Add(new StringContent(childDTO.IsParent.ToString()), "isParent");

            var response = await httpClient.PostAsync("http://FaceRecognition:54621/childs/search", content);
            var responseStringContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var ErrorResponse = JsonSerializer.Deserialize<FindNearestErrorResponse>(responseStringContent);
                return new FindNearestModel
                {
                    StautsCode = (int)response.StatusCode
                };
            }
            var findNearestResponse = JsonSerializer.Deserialize<FindNearestModel>(responseStringContent);

            return new FindNearestModel
            {
                Image = findNearestResponse.Image,
                Id = findNearestResponse.Id,
                IsParent = findNearestResponse.IsParent,
                StautsCode = (int)response.StatusCode,
                Date = findNearestResponse.Date
            };

        }


    }
}
