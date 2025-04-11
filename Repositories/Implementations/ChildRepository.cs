using Microsoft.EntityFrameworkCore;
using Reunite.Data;
using Reunite.Models.Children;
using Reunite.Repositories.Interfaces;

namespace Reunite.Repositories.Implementations
{
    public class ChildRepository : IChildRepository
    {

        private readonly ReuniteDbContext context;

        public ChildRepository(ReuniteDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Child>> GetChilds()
        {
            return await context.Childs.Include(c => c.User).ToListAsync();
        }

        public async Task<Child> GetChild(string id)
        {
            return await context.Childs.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == id); ;
        }

        public async Task AddChild(Child foundChild)
        {
            await context.Childs.AddAsync(foundChild);
            await context.SaveChangesAsync();
        }

        public async Task UpdateChildAsync(string id, Child newChild)
        {
            var child = await GetChild(id);
            child.UserId = newChild.UserId;
            await context.SaveChangesAsync();
        }

        public async Task DeleteChild(string id)
        {
            var child = await GetChild(id);
            context.Childs.Remove(child);
            await context.SaveChangesAsync();
        }

    }
}