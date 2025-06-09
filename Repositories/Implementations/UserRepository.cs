using Microsoft.EntityFrameworkCore;
using Reunite.Data;
using Reunite.Models;
using Reunite.Repositories.Interfaces;

namespace Reunite.Repositories.Implementations
{
    public class UserRepository(ReuniteDbContext context) : IUserRepository
    {

        private readonly ReuniteDbContext context = context;

        public async Task<List<ReuniteUser>> GetUsersAsync() => await context.Users.Include(u => u.Queries).ToListAsync();

        public async Task<ReuniteUser?> GetUserAsync(string id) => await context.Users.Include(u => u.Queries).FirstOrDefaultAsync(c => c.Id == id);

        public async Task AddUserAsync(ReuniteUser user)
        {
            context.Users.Add(user);
            await SaveChangesAsync();
        }

        public async Task UpdateUserAsync(string id, ReuniteUser newUser)
        {
            var existingUser = await GetUserAsync(id);

            if (existingUser is null)
                return;

            context.Entry(existingUser).CurrentValues.SetValues(new { newUser.Username, newUser.Email });

            await SaveChangesAsync();
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await GetUserAsync(id);

            if (user is null)
                return false;

            context.Users.Remove(user);
            await SaveChangesAsync();

            return true;
        }

        public async Task SaveChangesAsync() => await context.SaveChangesAsync();

    }
}