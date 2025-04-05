using Reunite.Models;

namespace Reunite.Repositories.Interfaces
{
    public interface IMissedChildRepository
    {
        Task<List<MissedChild>> GetMissedChilds();
        Task<MissedChild> GetMissedChild(string id);
        void UpdateMissedChild(MissedChild newChild, string id);
        void DeleteMissedChild(string id);
        Task<MissedChild> AddMissedChild(MissedChild missedChild, string id);

    }
}
