using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Reunite.DTOs.QueryDTOs;
using Reunite.Helpers;
using Reunite.Repositories.Interfaces;
using Reunite.Services.Interfaces;
using Reunite.Shared;
using Reunite.Models;

namespace Reunite.Services.Implementations
{
    public class QueryService(
        HttpClient httpClient,
        IConfiguration configuration,
        IQueryRepository queryRepository,
        IChatService chatService) : IQueryService
    {
        public async Task<Result<FindNearestDTO>> FindNearest(SearchDTO searchDTO, bool isParent)
        {
            using var content = QueryServiceHelpers.CreateMultipartFormData(searchDTO, isParent);

            string URL = $"http://{configuration["AI:IP"]}:{configuration["AI:Port"]}/childs/search";

            var response = await httpClient.PostAsync(URL, content);
            if (!response.IsSuccessStatusCode)
                return Result<FindNearestDTO>.Failure("No similar images", 404);

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();

            string? id = json.GetProperty("_id").GetString();
            string? Date = json.GetProperty("date").GetString();
            string? image = json.GetProperty("image").GetString();

            var query = await queryRepository.GetQueryAsync(id!);

            return Result<FindNearestDTO>.Success(new FindNearestDTO
            {
                Id = id!,
                IsParent = !isParent,
                Date = Date!,
                Image = image!,
                ReceiverId = query!.User.Id,
                ReceiverUsername = query.User.Username,
                Longitude = query.Location.Longitude!,
                Latitude = query.Location.Latitude!,
                ChatId = await chatService.OpenChatBetweenUsersAsync(query!.User.Id, searchDTO.UserId)
            }, 200);
        }


        public async Task<QueryDTO> AddQueryByParent(ParentSearchDTO searchDto)
        {
            var imageId = Guid.NewGuid().ToString();
            await UploadImageToAIService(searchDto.Image, true, imageId);
            var result = await GetImage(imageId);

            var query = await queryRepository.AddQueryAsync(new Query
            {
                UserId = searchDto.UserId,
                ChildName = searchDto.ChildName,
                ChildAge = searchDto.ChildAge,
                Id = imageId,
                ChildImage = result.Data,
                IsParent = true
            });
            return MapToQueryDto(query);
        }


        public async Task<QueryDTO> AddQueryByFinder(FinderSearchDTO searchDto)
        {
            var imageId = Guid.NewGuid().ToString();
            await UploadImageToAIService(searchDto.Image, false, imageId);
            var result = await GetImage(imageId);

            Location location = new Location()
                { Id = Guid.NewGuid().ToString(), Latitude = searchDto.Latitude, Longitude = searchDto.Longitude };

            var query = await queryRepository.AddQueryAsync(new Query
            {
                UserId = searchDto.UserId,
                Id = imageId,
                Location = location,
                ChildImage = result.Data,
                IsParent = false
            });
            return MapToQueryDto(query);
        }

        private async Task UploadImageToAIService(IFormFile image, bool isParent, string imageId)
        {
            var content = AIServiceHelper.CreateMultipartContent(image, isParent);
            content.Add(new StringContent(imageId), "id");

            await httpClient.PostAsync(
                $"http://{configuration["AI:IP"]}:{configuration["AI:Port"]}/childs/",
                content);
        }

        private async Task<Result<string>> GetImage(string imageId)
        {
            string URL = $"http://{configuration["AI:IP"]}:{configuration["AI:Port"]}/childs/{imageId}";
            var response = await httpClient.GetAsync(URL);
            if (!response.IsSuccessStatusCode)
                return Result<string>.Failure("No similar images", 404);

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            string? image = json.GetProperty("image").GetString();
            return Result<string>.Success(image, 200);
        }

        private QueryDTO MapToQueryDto(Query query)
        {
            return new QueryDTO()
            {
                ChildImage = query.ChildImage,
                ChildName = query.ChildName,
                ChildAge = query.ChildAge,
                CreatedAt = query.CreatedAt,
                IsCompleted = query.IsCompleted,
                Id = query.Id
            };
        }
    }
}