using Reunite.Models.Children;

namespace Reunite.Repositories.Interfaces
{
    public interface IMissedChildRepository
    {
        Task<List<MissedChild>> GetMissedChilds();
        Task<MissedChild> GetMissedChild(string id);
        Task AddMissedChild(MissedChild missedChild);
        Task UpdateMissedChild(string id, MissedChild newChild);
        Task DeleteMissedChild(string id);
    }
}