using Reunite.DTOs.QueryDTOs;
using Reunite.Shared;

namespace Reunite.Services.Interfaces
{
    public interface IQueryService
    {
        Task<Result<FindNearestDTO>> FindNearest(SearchDTO searchDTO);





        

        //Task AddChildByParent(ParentSearchDTO searchDto);
        //Task AddChildByFinder(FinderSearchDTO searchDto);
    }
}