using Microsoft.AspNetCore.SignalR;
using Reunite.Models;
using Reunite.Repositories.Interfaces;
using Reunite.Services.Interfaces;

namespace Reunite.Hubs
{
    public class ChatHub : Hub
    {

        private readonly IMessageRepository messageRepository = null!;
        private readonly IUserRepository userRepository = null!;
        private readonly IFirebaseNotificationService notificationService = null!;

        public ChatHub(IMessageRepository messageRepository, IUserRepository userRepository, IFirebaseNotificationService notificationService)
        {
            this.messageRepository = messageRepository;
            this.userRepository = userRepository;
            this.notificationService = notificationService;
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
            var receiver = await userRepository.GetUserAsync(receiverId);
            if (!string.IsNullOrEmpty(receiver?.FcmToken))
            {
                await notificationService.SendNotification(
                    receiver.FcmToken,
                    "New message",
                    $"{user.Username}: {content}"
                );
            }
            var senderUsername = user!.Username;
            var lastMessage = content;
            var lastMessageTime = message.SentAt;

            await Clients.User(receiverId).SendAsync("UpdateChatList", senderId, senderUsername, lastMessage, lastMessageTime);

        }

    }
}