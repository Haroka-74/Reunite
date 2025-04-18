using Microsoft.EntityFrameworkCore;
using Reunite.Data;
using Reunite.Models.Auth;
using Reunite.Repositories.Interfaces;

namespace Reunite.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {

        private readonly ReuniteDbContext context;

        public UserRepository(ReuniteDbContext context)
        {
            this.context = context;
        }

        public async Task<List<ReuniteUser>> GetUsers()
        {
            return await context.Users.Include(u => u.Childs).ToListAsync();
        }

        public async Task<ReuniteUser> GetUser(string id)
        {
            return await context.Users.Include(u => u.Childs).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddUser(ReuniteUser user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task UpdateUser(string id, ReuniteUser newUser)
        {
            var user = await GetUser(id);
            user.Username = newUser.Username;
            user.Email = newUser.Email;
            await context.SaveChangesAsync();
        }

        public async Task DeleteUser(string id)
        {
            var user = await GetUser(id);
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }

    }
}