using Microsoft.EntityFrameworkCore;
using Reunite.Data;
using Reunite.Models.Children;
using Reunite.Repositories.Interfaces;

namespace Reunite.Repositories.Implementations
{
    public class FoundChildRepository : IFoundChildRepository
    {

        private readonly ReuniteDbContext context;

        public FoundChildRepository(ReuniteDbContext context)
        {
            this.context = context;
        }

        public async Task<List<FoundChild>> GetFoundChilds()
        {
            return await context.FoundChilds.Include(c => c.User).ToListAsync();
        }

        public async Task<FoundChild> GetFoundChild(string id)
        {
            return await context.FoundChilds.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == id); ;
        }

        public async Task AddFoundChild(FoundChild foundChild)
        {
            await context.FoundChilds.AddAsync(foundChild);
            await context.SaveChangesAsync();
        }

        public async Task UpdateFoundChildAsync(string id, FoundChild newChild)
        {
            var child = await GetFoundChild(id);
            child.UserId = newChild.UserId;
            await context.SaveChangesAsync();
        }

        public async Task DeleteFoundChild(string id)
        {
            var child = await GetFoundChild(id);
            context.FoundChilds.Remove(child);
            await context.SaveChangesAsync();
        }

    }
}