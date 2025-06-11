using Reunite.Models;

namespace Reunite.Repositories.Interfaces
{
    public interface IChatRepository
    {
        Task<Chat?> GetChatAsync(string userId1, string userId2);
        Task<Chat?> GetChatAsync(string chatId);
        Task<List<Chat>> GetUserChatsAsync(string userId);
        Task CreateChatAsync(Chat chat);
    }
}