using Microsoft.AspNetCore.SignalR;
using Reunite.Models;
using Reunite.Repositories.Interfaces;

namespace Reunite.Hubs
{
    public class ChatHub : Hub
    {

        private readonly IMessageRepository messageRepository = null!;

        public ChatHub(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
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

            await Clients.Group(chatId).SendAsync("ReceiveMessage", chatId, senderId, receiverId, content);
        }

    }
}