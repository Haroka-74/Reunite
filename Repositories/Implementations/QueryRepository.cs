using Microsoft.EntityFrameworkCore;
using Reunite.Data;
using Reunite.Models;
using Reunite.Repositories.Interfaces;

namespace Reunite.Repositories.Implementations
{
    public class QueryRepository(ReuniteDbContext context) : IQueryRepository
    {
        public async Task<List<Query>> GetQueriesAsync() =>
            await context.Queries.Include(q => q.Location).Include(q => q.FacebookPost).ToListAsync();

        public async Task<Query?> GetQueryAsync(string id) => await context.Queries.Include(q => q.User)
            .Include(q => q.Location).Include(q => q.FacebookPost).FirstOrDefaultAsync(q => q.Id == id);

        public async Task<Query> AddQueryAsync(Query query)
        {
            context.Queries.Add(query);
            await SaveChangesAsync();
            return query;
        }

        public async Task UpdateQueryAsync(string id, Query newQuery)
        {
            var existingQuery = await GetQueryAsync(id);

            if (existingQuery is null)
                return;

            if (newQuery.ChildName is not null) existingQuery.ChildName = newQuery.ChildName;
            if (newQuery.ChildAge is not null) existingQuery.ChildAge = newQuery.ChildAge;
            if (newQuery.Location is not null)
            {
                existingQuery.Location = new Location
                {
                    Latitude = newQuery.Location.Latitude,
                    Longitude = newQuery.Location.Longitude
                };
            }

            await SaveChangesAsync();
        }

        public async Task<bool> DeleteQueryAsync(string id)
        {
            var query = await GetQueryAsync(id);

            if (query is null)
                return false;

            context.Queries.Remove(query);
            await SaveChangesAsync();

            return true;
        }

        public async Task<List<Query>> GetUserQueriesAsync(string userId)
        {
            return await context.Queries
                .Include(q => q.Location)
                .Include(q => q.FacebookPost)
                .Where(q => q.UserId == userId)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }

        public async Task SaveChangesAsync() => await context.SaveChangesAsync();
    }
}