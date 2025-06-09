using System.Text.Json;
using Reunite.DTOs.QueryDTOs;
using Reunite.Helpers;
using Reunite.Repositories.Interfaces;
using Reunite.Services.Interfaces;
using Reunite.Shared;

namespace Reunite.Services.Implementations
{
    public class QueryService(HttpClient httpClient, IConfiguration configuration, IQueryRepository queryRepository, IChatService chatService) : IQueryService
    {

        private readonly HttpClient httpClient = httpClient;
        private readonly IQueryRepository queryRepository = queryRepository;
        private readonly IChatService chatService = chatService;
        private readonly IConfiguration configuration = configuration;

        public async Task<Result<FindNearestDTO>> FindNearest(SearchDTO searchDTO)
        {
            using var content = QueryServiceHelpers.CreateMultipartFormData(searchDTO);

            string URL = $"http://{configuration["AI:IP"]}:{configuration["AI:Port"]}/childs/search";

            var response = await httpClient.PostAsync(URL, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return Result<FindNearestDTO>.Failure("No similar images", 404);

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();

            string? id = json.GetProperty("_id").GetString();
            bool isParent = json.GetProperty("isParent").GetBoolean();
            string? Date = json.GetProperty("date").GetString();
            string? image = json.GetProperty("image").GetString();

            var query = await queryRepository.GetQueryAsync(id!);
            
            return Result<FindNearestDTO>.Success(new FindNearestDTO
            {
                Id = id!,
                IsParent = isParent,
                Date = Date!,
                Image = image!,
                ReceiverId = query!.User.Id,
                ReceiverUsername = query.User.Username,
                Location = query.Location!,
                ChatId = await chatService.OpenChatBetweenUsersAsync(query!.User.Id, searchDTO.UserId)
            }, 200);
        }






        //public async Task AddChildByParent(ParentSearchDTO searchDto)
        //{
        //    var imageId = Guid.NewGuid().ToString();
        //    await UploadImageToAiService(searchDto.Image, searchDto.IsParent, imageId);

        //    await childRepository.AddQueryAsync(new Query
        //    {
        //        UserId = searchDto.UserId,
        //        Name = searchDto.ChildName,
        //        Age = searchDto.ChildAge,
        //        Id = imageId
        //    });
        //}



        //public async Task AddChildByFinder(FinderSearchDTO searchDto)
        //{
        //    var imageId = Guid.NewGuid().ToString();
        //    await UploadImageToAiService(searchDto.Image, searchDto.IsParent, imageId);

        //    await childRepository.AddQueryAsync(new Query
        //    {
        //        UserId = searchDto.UserId,
        //        Id = imageId,
        //        Location = searchDto.Location
        //    });
        //}

        //private async Task UploadImageToAiService(IFormFile image, bool isParent, string imageId)
        //{
        //    var content = AiServiceHelper.CreateMultipartContent(image, isParent);
        //    content.Add(new StringContent(imageId), "id");

        //    await httpClient.PostAsync(
        //        $"http://{configuration["AI:IP"]}:{configuration["AI:Port"]}/childs/",
        //        content);
        //}

    }
}
