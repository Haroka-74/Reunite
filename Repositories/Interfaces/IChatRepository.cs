using Reunite.Models.Chats;

namespace Reunite.Repositories.Interfaces
{
    public interface IChatRepository
    {
        Task<List<Chat>> GetChatsAsync();
        Task<Chat> GetChatAsync(string chatId);
        Task CreateChatAsync(Chat chat);
    }
}