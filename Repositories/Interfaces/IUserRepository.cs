using Reunite.Models;

namespace Reunite.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<ReuniteUser>> GetUsersAsync();
        Task<ReuniteUser?> GetUserAsync(string id);
        Task AddUserAsync(ReuniteUser user);
        Task UpdateUserAsync(string id, ReuniteUser newUser);
        Task<bool> DeleteUserAsync(string id);
        Task SaveChangesAsync();
    }
}