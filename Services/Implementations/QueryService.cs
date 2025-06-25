using System.Text.Json;
using Reunite.DTOs.QueryDTOs;
using Reunite.Helpers;
using Reunite.Repositories.Interfaces;
using Reunite.Services.Interfaces;
using Reunite.Shared;
using Reunite.Models;
using Reunite.DTOs.SearchDTOs;

namespace Reunite.Services.Implementations
{
    public class QueryService(
        HttpClient httpClient,
        IConfiguration configuration,
        IQueryRepository queryRepository,
        IChatService chatService,
        IUserRepository userRepository) : IQueryService
    {
        public async Task<Result<FindNearestDTO>> FindNearest(SearchDTO searchDTO, bool isParent)
        {
            using var content = QueryServiceHelpers.CreateMultipartFormData(searchDTO, isParent);

            string URL = $"http://{configuration["AI:IP"]}:{configuration["AI:Port"]}/childs/search";

            var response = await httpClient.PostAsync(URL, content);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return Result<FindNearestDTO>.Failure("Invalid picture: No faces detected in the image", 400);

            if (!response.IsSuccessStatusCode)
                return Result<FindNearestDTO>.Failure("No similar images, we add your child image into database", 201);

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();

            string? id = json.GetProperty("_id").GetString();
            string? Date = json.GetProperty("date").GetString();
            string? image = json.GetProperty("image").GetString();

            var query = await queryRepository.GetQueryAsync(id!);

            if (query!.User.Id == searchDTO.UserId)
                return Result<FindNearestDTO>.Failure("You cannot parent and finder in the same time", 400);

            return Result<FindNearestDTO>.Success(new FindNearestDTO
            {
                Id = id!,
                IsParent = !isParent,
                Date = Date!,
                Image = image!,
                ReceiverId = query!.User.Id,
                ReceiverUsername = query.User.Username,
                Longitude = query.Location?.Longitude,
                Latitude = query.Location?.Latitude,
                ChatId = await chatService.OpenChatBetweenUsersAsync(query!.User.Id, searchDTO.UserId)
            }, 200);
        }


        public async Task<QueryDTO> AddQueryByParent(ParentSearchDTO searchDto)
        {
            var imageId = Guid.NewGuid().ToString();
            string? imageBase64 = await UploadImageToAIService(searchDto.Image, true, imageId);

            var query = await queryRepository.AddQueryAsync(new Query
            {
                UserId = searchDto.UserId,
                ChildName = searchDto.ChildName,
                ChildAge = searchDto.ChildAge,
                Id = imageId,
                ChildImage = imageBase64!,
                IsParent = true
            });
            return MapToQueryDto(query);
        }


        public async Task<QueryDTO> AddQueryByFinder(FinderSearchDTO searchDto)
        {
            var imageId = Guid.NewGuid().ToString();
            string? imageBase64 = await UploadImageToAIService(searchDto.Image, false, imageId);

            Location location = new Location()
                { Id = Guid.NewGuid().ToString(), Latitude = searchDto.Latitude, Longitude = searchDto.Longitude };

            var query = await queryRepository.AddQueryAsync(new Query
            {
                UserId = searchDto.UserId,
                Id = imageId,
                Location = location,
                ChildImage = imageBase64!,
                IsParent = false
            });
            return MapToQueryDto(query);
        }

        public async Task<Result<List<QueryDTO>>> GetUserQueriesAsync(string userId)
        {
            if (await userRepository.GetUserAsync(userId) is null)
            {
                return Result<List<QueryDTO>>.Failure("User not Found", 404);
            }

            var userQueries = await queryRepository.GetUserQueriesAsync(userId);
            var userList = userQueries.Select(q => MapToQueryDto(q)).ToList();
            return Result<List<QueryDTO>>.Success(userList, 200);
        }

        public async Task<Result<QueryDTO>> GetQueryAsync(string queryId)
        {
            var query = await queryRepository.GetQueryAsync(queryId);
            if (query is null)
            {
                return Result<QueryDTO>.Failure("Query not Found", 404);
            }

            return Result<QueryDTO>.Success(MapToQueryDto(query), 200);
        }
        
        public async Task<Result<QueryDTO>> ChangeIsCompleted(string queryId)
        {
            var query = await queryRepository.UpdateQueryAsync(queryId,new Query(){IsCompleted = true});
            if (query is null)
            {
                return Result<QueryDTO>.Failure("Query not Found", 404);
            }

            return Result<QueryDTO>.Success(MapToQueryDto(query), 200);
        }

        private async Task<string?> UploadImageToAIService(IFormFile image, bool isParent, string imageId)
        {
            var content = AIServiceHelper.CreateMultipartContent(image, isParent);
            content.Add(new StringContent(imageId), "id");

            var response = await httpClient.PostAsync($"http://{configuration["AI:IP"]}:{configuration["AI:Port"]}/childs/", content);

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            var jsonObject = JsonSerializer.Deserialize<JsonElement>(jsonString);
            string? imageBase64 = jsonObject.GetProperty("image").GetString();

            return imageBase64;
        }

        private static QueryDTO MapToQueryDto(Query query)
        {
            return new QueryDTO()
            {
                ChildImage = query.ChildImage,
                ChildName = query?.ChildName ?? "Unknown",
                ChildAge = query?.ChildAge ?? 0,
                CreatedAt = query.CreatedAt,
                IsCompleted = query.IsCompleted,
                Id = query.Id,
                FacebookLink = query.FacebookPost?.Link ?? "No facebook post",
                Longitude = query.Location?.Longitude ?? 0,
                Latitude = query.Location?.Latitude ?? 0,
            };
        }
    }
}