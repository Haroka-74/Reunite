using Reunite.DTOs.ChatDTOs;

namespace Reunite.Services.Interfaces
{
    public interface IChatService
    {
        Task<string> OpenChatBetweenUsersAsync(string userId1, string userId2);
        Task<List<MessageDTO>> GetChatMessages(string chatId);
        Task<int> GetUnreadMessagesCountAsync(string userId);
    }
}