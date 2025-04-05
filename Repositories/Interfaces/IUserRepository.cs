using Reunite.Models;

namespace Reunite.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(string id);
        void UpdateUser(User newUser, string id);
        void DeleteUser(string id);
        Task<User> AddUser(User user);

    }
}
