using System.Net.Http.Headers;
using System.Text.Json;
using Reunite.Domain;
using Reunite.DTOs;
using Reunite.Helpers;
using Reunite.Models.Children;
using Reunite.Repositories.Interfaces;
using Reunite.Services.Interfaces;

namespace Reunite.Services.Implementations
{
    public class ChildService : IChildService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly IChildRepository childRepository;

        public ChildService(HttpClient httpClient, IConfiguration configuration, IChildRepository childRepository)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
            this.childRepository = childRepository;
        }
        public async Task<FindNearestResponse> FindNearest(SearchDTO searchDto)
        {

            using var content = new MultipartFormDataContent();

            var imageContent = new StreamContent(searchDto.Image.OpenReadStream());
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(searchDto.Image.ContentType);
            content.Add(imageContent, "image", searchDto.Image.FileName);

            content.Add(new StringContent(searchDto.IsParent.ToString()), "isParent");

            var response = await httpClient.PostAsync($"http://{configuration["AI:IP"]}:{configuration["AI:Port"]}/childs/search", content);
            var responseStringContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = JsonSerializer.Deserialize<FindNearestErrorResponse>(responseStringContent);
                errorResponse.StatusCode = (int) response.StatusCode;
                return new FindNearestResponse
                {
                    Error = errorResponse
                };
            }
            var successResponse = JsonSerializer.Deserialize<FindNearestSuccessResponse>(responseStringContent);

            return new FindNearestResponse
            {
                Success = successResponse
            };

        }
        public async Task AddChildByParent(ParentSearchDTO searchDto)
        {
            var imageId = Guid.NewGuid().ToString();
            await UploadImageToAiService(searchDto.Image, searchDto.IsParent, imageId);

            await childRepository.AddChild(new Child
            {
                UserId = searchDto.UserId,
                Name = searchDto.ChildName,
                Age = searchDto.ChildAge,
                Id = imageId
            });
        }

        public async Task AddChildByFinder(FinderSearchDTO searchDto)
        {
            var imageId = Guid.NewGuid().ToString();
            await UploadImageToAiService(searchDto.Image, searchDto.IsParent, imageId);

            await childRepository.AddChild(new Child
            {
                UserId = searchDto.UserId,
                Id = imageId
            });
        }



        private async Task UploadImageToAiService(IFormFile image, bool isParent, string imageId)
        {
            var content = AiServiceHelper.CreateMultipartContent(image, isParent);
            content.Add(new StringContent(imageId), "id");

            await httpClient.PostAsync(
                $"http://{configuration["AI:IP"]}:{configuration["AI:Port"]}/childs/",
                content);
        }

    }
}
