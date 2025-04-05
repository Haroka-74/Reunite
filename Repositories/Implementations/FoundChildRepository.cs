using Microsoft.EntityFrameworkCore;
using Reunite.Data;
using Reunite.Models;
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
        public async Task<FoundChild> AddFoundChild(FoundChild foundChild)
        {
            context.FoundChilds.Add(foundChild);
            await context.SaveChangesAsync();
            return foundChild;
        }

        public async void DeleteFoundChild(string id)
        {
            FoundChild child = await GetFoundChild(id);
            if (child == null) return;
            context.FoundChilds.Remove(child);
            await context.SaveChangesAsync();
        }

        public async Task<List<FoundChild>> GetFoundChilds()
        {
            return await context.FoundChilds.Include(c => c.User).ToListAsync();
        }

        public async Task<FoundChild> GetFoundChild(string id)
        {
            FoundChild child = await context.FoundChilds.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == id);
            if (child == null) return null;
            return child;

        }

        public async void UpdateFoundChildAsync(FoundChild newChild, string id)
        {
            FoundChild child = await GetFoundChild(id);
            if (child == null) return;
            child.UserId = newChild.UserId;
            await context.SaveChangesAsync();
        }


    }
}
