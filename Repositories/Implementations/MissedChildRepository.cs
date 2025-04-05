using Microsoft.EntityFrameworkCore;
using Reunite.Data;
using Reunite.Models;
using Reunite.Repositories.Interfaces;

namespace Reunite.Repositories.Implementations
{

    public class MissedChildRepository : IMissedChildRepository
    {
        private readonly ReuniteDbContext context;

        public MissedChildRepository(ReuniteDbContext context)
        {
            this.context = context;
        }
        public async Task<MissedChild> AddMissedChild(MissedChild missedChild)
        {
            context.MissedChilds.Add(missedChild);
            await context.SaveChangesAsync();
            return missedChild;

        }

        public async void DeleteMissedChild(string id)
        {
            MissedChild missedChild = await GetMissedChild(id);
            context.MissedChilds.Remove(missedChild);
            await context.SaveChangesAsync();
        }

        public async Task<MissedChild> GetMissedChild(string id)
        {
            MissedChild child = await context.MissedChilds.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == id);
            if (child == null) return null;
            return child;
        }

        public async Task<List<MissedChild>> GetMissedChilds()
        {
            return await context.MissedChilds.Include(c => c.User).ToListAsync();
        }

        public async void UpdateMissedChild(MissedChild newChild, string id)
        {
            MissedChild child = await GetMissedChild(id);
            if (child == null) return;
            child.UserId = newChild.UserId;
            child.Age = newChild.Age;
            child.Name = newChild.Name;
            await context.SaveChangesAsync();
        }
    }
}
