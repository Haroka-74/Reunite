using Microsoft.AspNetCore.SignalR;
using Reunite.Models;
using Reunite.Repositories.Implementations;
using Reunite.Repositories.Interfaces;

namespace Reunite.Hubs
{
    public class ChatHub : Hub
    {

        private readonly IMessageRepository messageRepository = null!;
        private readonly IUserRepository userRepository = null!;

        public ChatHub(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            this.messageRepository = messageRepository;
            this.userRepository = userRepository;
        }

        public async Task JoinGroup(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }

        public async Task SendMessage(string chatId, string senderId, string receiverId, string content)
        {
            var message = new Message
            {
                Id = Guid.NewGuid().ToString(),
                Content = content,
                SenderId = senderId,
                ChatId = chatId
            };

            await messageRepository.AddMessageAsync(message);

            await Clients.Group(chatId).SendAsync("ReceiveMessage", senderId, content);

            var user = await userRepository.GetUserAsync(senderId);

            var senderUsername = user!.Username;
            var lastMessage = content;
            var lastMessageTime = message.SentAt;

            await Clients.User(receiverId).SendAsync("UpdateChatList", senderId, senderUsername, lastMessage, lastMessageTime);
        }

    }
}