using Microsoft.EntityFrameworkCore;
using Reunite.Data;
using Reunite.Models.Children;
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

        public async Task<List<MissedChild>> GetMissedChilds()
        {
            return await context.MissedChilds.Include(c => c.User).ToListAsync();
        }

        public async Task<MissedChild> GetMissedChild(string id)
        {
            return await context.MissedChilds.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddMissedChild(MissedChild missedChild)
        {
            await context.MissedChilds.AddAsync(missedChild);
            await context.SaveChangesAsync();
        }

        public async Task UpdateMissedChild(string id, MissedChild newChild)
        {
            var child = await GetMissedChild(id);
            child.Name = newChild.Name;
            child.Age = newChild.Age;
            child.UserId = newChild.UserId;
            await context.SaveChangesAsync();
        }

        public async Task DeleteMissedChild(string id)
        {
            var missedChild = await GetMissedChild(id);
            context.MissedChilds.Remove(missedChild);
            await context.SaveChangesAsync();
        }

    }
}