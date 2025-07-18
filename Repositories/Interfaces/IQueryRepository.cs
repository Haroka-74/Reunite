﻿using Reunite.Models;

namespace Reunite.Repositories.Interfaces
{
    public interface IQueryRepository
    {
        Task<List<Query>> GetQueriesAsync();
        Task<Query?> GetQueryAsync(string id);
        Task<Query> AddQueryAsync(Query query);
        Task<Query?> UpdateQueryAsync(string id, Query query);
        Task<bool> DeleteQueryAsync(string id);
        Task<List<Query>> GetUserQueriesAsync(string userId);
        Task SaveChangesAsync();
    }
}