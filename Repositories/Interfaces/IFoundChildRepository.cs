using Reunite.Models;

namespace Reunite.Repositories.Interfaces
{
    public interface IFoundChildRepository
    {
        Task<List<FoundChild>> GetFoundChilds();
        Task<FoundChild> GetFoundChild(string id);

        void UpdateFoundChildAsync(FoundChild newChild, string id);
        void DeleteFoundChild(string id);

        Task<FoundChild> AddFoundChild(FoundChild foundChild);
    }
}
