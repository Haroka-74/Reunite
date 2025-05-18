using Reunite.Models;

namespace Reunite.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<ReuniteUser>> GetUsers();
        Task<ReuniteUser> GetUser(string id);
        Task AddUser(ReuniteUser user);
        Task UpdateUser(string id, ReuniteUser newUser);
        Task DeleteUser(string id);
    }
}