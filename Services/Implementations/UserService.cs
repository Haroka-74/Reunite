using Reunite.DTOs.ChatDTOs;
using Reunite.Repositories.Interfaces;
using Reunite.Services.Interfaces;

namespace Reunite.Services.Implementations
{
    public class UserService : IUserService
    {

        private readonly IChatRepository chatRepository = null!;

        public UserService(IChatRepository chatRepository)
        {
            this.chatRepository = chatRepository;
        }

        public async Task<List<ChatDTO>> GetUserChatsAsync(string userId)
        {
            var chats = await chatRepository.GetUserChatsAsync(userId);

            return [.. chats.Select(c => new ChatDTO
                {
                    ChatId = c.Id,
                    ReceiverId = c.UserId1 == userId ? c.UserId2 : c.UserId1,
                    ReceiverUsername = c.UserId1 == userId ? c.User2.Username : c.User1.Username,
                    LastMessage = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault().Content,
                    LastMessageTime = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault().SentAt,
                    UnreadCount = c.UserId1 == userId ? c.UnreadCountUser1 : c.UnreadCountUser2
                })
                .OrderByDescending(c => c.LastMessageTime)];
        }

    }
}