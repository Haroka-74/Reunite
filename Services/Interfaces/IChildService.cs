using Reunite.Domain;
using Reunite.DTOs;
using Reunite.Models;

namespace Reunite.Services.Interfaces
{
    public interface IChildService
    {
        Task<FindNearestResponse> FindNearest(SearchDTO searchDto);
        Task AddChild(SearchDTO searchDto);

    }
}