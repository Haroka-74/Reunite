using Microsoft.EntityFrameworkCore;
using Reunite.Data;
using Reunite.Models;
using Reunite.Repositories.Interfaces;

namespace Reunite.Repositories.Implementations
{
    public class QueryRepository : IQueryRepository
    {

        private readonly ReuniteDbContext context;

        public QueryRepository(ReuniteDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Query>> GetQueriesAsync()
        {
            return await context.Queries.Include(c => c.User).ToListAsync();
        }

        public async Task<Query> GetQueryAsync(string id)
        {
            return await context.Queries.Include(c => c.User)
                .Include(c => c.Location)
                .Include(c => c.FacebookPost)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddQueryAsync(Query foundChild)
        {
            await context.Queries.AddAsync(foundChild);
            await context.SaveChangesAsync();
        }

        public async Task UpdateQueryAsync(string id, Query newChild)
        {
            var child = await GetQueryAsync(id);
            child.UserId = newChild.UserId;
            await context.SaveChangesAsync();
        }

        public async Task DeleteQueryAsync(string id)
        {
            var child = await GetQueryAsync(id);
            context.Queries.Remove(child);
            await context.SaveChangesAsync();
        }

        public async Task<List<Query>> GetUserQueriesAsync(string userId)
        {
            return await context.Queries.Include(c => c.User)
                .Include(c => c.Location)
                .Include(c => c.FacebookPost)
                .Where(c => c.UserId == userId).ToListAsync();
        }
    }
}