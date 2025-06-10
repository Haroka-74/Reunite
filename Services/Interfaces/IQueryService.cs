using Reunite.DTOs.QueryDTOs;
using Reunite.Shared;

namespace Reunite.Services.Interfaces
{
    public interface IQueryService
    {
        Task<Result<FindNearestDTO>> FindNearest(SearchDTO searchDTO, bool isParent);
        Task<QueryDTO> AddQueryByParent(ParentSearchDTO searchDto);
        Task<QueryDTO> AddQueryByFinder(FinderSearchDTO searchDto);

        Task<Result<List<QueryDTO>>> GetUserQueriesAsync(string userId);
        Task<Result<QueryDTO>> GetQueryAsync(string queryId);
        Task<Result<QueryDTO>> ChangeIsCompleted(string queryId);
    }
}