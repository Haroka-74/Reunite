using Reunite.Models.Children;

namespace Reunite.Repositories.Interfaces
{
    public interface IFoundChildRepository
    {
        Task<List<FoundChild>> GetFoundChilds();
        Task<FoundChild> GetFoundChild(string id);
        Task AddFoundChild(FoundChild foundChild);
        Task UpdateFoundChildAsync(string id, FoundChild newChild);
        Task DeleteFoundChild(string id);
    }
}