using Reunite.DTOs.QueryDTOs;
using Reunite.Shared;

namespace Reunite.Services.Interfaces
{
    public interface IQueryService
    {
        Task<Result<FindNearestDTO>> FindNearest(SearchDTO searchDTO,bool isParent);
        Task<QueryDTO>  AddQueryByParent(ParentSearchDTO searchDto);
        Task<QueryDTO>  AddQueryByFinder(FinderSearchDTO searchDto);
    }
}