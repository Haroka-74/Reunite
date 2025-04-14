using Reunite.Domain;
using Reunite.DTOs;

namespace Reunite.Services.Interfaces
{
    public interface IChildService
    {
        Task<FindNearestResponse> FindNearest(SearchDTO searchDto);
        Task AddChildByParent(ParentSearchDTO searchDto);
        Task AddChildByFinder(FinderSearchDTO searchDto);

    }
}