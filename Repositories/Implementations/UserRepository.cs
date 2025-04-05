using Microsoft.EntityFrameworkCore;
using Reunite.Data;
using Reunite.Models;
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
        public async Task<User> AddUser(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async void DeleteUser(string id)
        {
            User user = await GetUser(id);
            if (user == null) return;
            context.Users.Remove(user);
            await context.SaveChangesAsync();

        }

        public async Task<User> GetUser(string id)
        {
            User user = await context.Users.Include(u => u.FoundChilds)
                .Include(u => u.MissedChilds)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (user == null) return null;
            return user;
        }

        public Task<List<User>> GetUsers()
        {
            return context.Users.Include(u => u.MissedChilds).Include(u => u.FoundChilds).ToListAsync();
        }

        public async void UpdateUser(User newUser, string id)
        {
            User user = await GetUser(id);
            if (user == null) return;
            user.Email = newUser.Email;
            user.PhoneNumber = newUser.PhoneNumber;
            user.Location = newUser.Location;
        }
    }
}
