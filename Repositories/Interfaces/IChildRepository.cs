using Reunite.Models.Children;

namespace Reunite.Repositories.Interfaces
{
    public interface IChildRepository
    {
        Task<List<Child>> GetChilds();
        Task<Child> GetChild(string id);
        Task AddChild(Child foundChild);
        Task UpdateChildAsync(string id, Child newChild);
        Task DeleteChild(string id);
    }
}