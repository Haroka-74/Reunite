using Reunite.DTOs.ChatDTOs;

namespace Reunite.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<ChatDTO>> GetUserChatsAsync(string userId);
    }
}