using Reunite.Models;

namespace Reunite.Repositories.Interfaces
{
    public interface IQueryRepository
    {
        Task<List<Query>> GetQueriesAsync();
        Task<List<Query>> GetUserQueriesAsync(string queryId);
        Task<Query> GetQueryAsync(string id);
        Task AddQueryAsync(Query foundChild);
        Task UpdateQueryAsync(string id, Query newChild);
        Task DeleteQueryAsync(string id);
    }
}