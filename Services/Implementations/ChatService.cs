using Reunite.DTOs.ChatDTOs;
using Reunite.Models;
using Reunite.Repositories.Interfaces;
using Reunite.Services.Interfaces;

namespace Reunite.Services.Implementations
{
    public class ChatService : IChatService
    {

        private readonly IChatRepository chatRepository = null!;

        public ChatService(IChatRepository chatRepository)
        {
            this.chatRepository = chatRepository;
        }

        public async Task<string> OpenChatBetweenUsersAsync(string userId1, string userId2)
        {
            var chats = await chatRepository.GetChatsAsync();

            var chat = chats.FirstOrDefault(c => (c.UserId1 == userId1 && c.UserId2 == userId2) || (c.UserId1 == userId2 && c.UserId2 == userId1));

            if (chat is null)
            {
                var newChat = new Chat
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId1 = userId1,
                    UserId2 = userId2
                };

                await chatRepository.CreateChatAsync(newChat);

                return newChat.Id;
            }

            return chat.Id;
        }

        public async Task<List<MessageDTO>> GetChatMessages(string chatId)
        {
            var chat = await chatRepository.GetChatAsync(chatId);
            return chat.Messages.OrderBy(m => m.SentAt).Select(m => new MessageDTO
            {
                SenderId = m.SenderId,
                Content = m.Content,
                SentAt = m.SentAt
            }).ToList();
        }

    }
}