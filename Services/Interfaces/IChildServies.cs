using Reunite.DTOs;
using Reunite.Models;

namespace Reunite.Services.Interfaces
{
    public interface IChildServies
    {
        Task<FindNearestModel> FindNearest(ChildDTO childDTO);
    }
}
