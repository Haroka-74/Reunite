using Microsoft.EntityFrameworkCore;
using Reunite.Data;
using Reunite.Models;
using Reunite.Repositories.Interfaces;
using System;

namespace Reunite.Repositories.Implementations
{
    public class ChatRepository(ReuniteDbContext context) : IChatRepository
    {

        public async Task<Chat?> GetChatAsync(string userId1, string userId2)
        {
            return await context.Chats
                .Include(c => c.User1)
                .Include(c => c.User2)
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => (c.UserId1 == userId1 && c.UserId2 == userId2) || (c.UserId1 == userId2 && c.UserId2 == userId1));
        }        

        public async Task<Chat?> GetChatAsync(string chatId)
        {
            return await context.Chats.Include(c => c.User1).Include(c => c.User2).Include(c => c.Messages).FirstOrDefaultAsync(c => c.Id == chatId);
        }

        public async Task<List<Chat>> GetUserChatsAsync(string userId)
        {
            return await context.Chats
                .Include(c => c.User1)
                .Include(c => c.User2)
                .Include(c => c.Messages)
                .Where(c => (c.UserId1 == userId || c.UserId2 == userId) && c.Messages.Any())
                .ToListAsync();
        }

        public async Task CreateChatAsync(Chat chat)
        {
            await context.Chats.AddAsync(chat);
            await context.SaveChangesAsync();
        }

        public async Task IncrementUnreadCountAsync(string chatId, string userId)
        {
            var chat = await context.Chats.FindAsync(chatId);
            if (chat != null)
            {
                if (chat.UserId1 == userId)
                {
                    chat.UnreadCountUser1++;
                }
                else if (chat.UserId2 == userId)
                {
                    chat.UnreadCountUser2++;
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task MarkMessagesAsReadAsync(string chatId, string userId)
        {
            var chat = await context.Chats.FindAsync(chatId);
            if (chat != null)
            {
                if (chat.UserId1 == userId)
                {
                    chat.UnreadCountUser1 = 0;
                }
                else if (chat.UserId2 == userId)
                {
                    chat.UnreadCountUser2 = 0;
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task<int> GetUnreadCountAsync(string chatId, string userId)
        {
            var chat = await context.Chats.FindAsync(chatId);
            if (chat != null)
            {
                if (chat.UserId1 == userId)
                {
                    return chat.UnreadCountUser1;
                }
                else if (chat.UserId2 == userId)
                {
                    return chat.UnreadCountUser2;
                }
            }

            return 0;
        }

        public async Task<int> GetTotalUnreadCountAsync(string userId)
        {
            var userChats = await context.Chats
                .Where(c => c.UserId1 == userId || c.UserId2 == userId)
                .ToListAsync();

            int totalUnread = 0;
            foreach (var chat in userChats)
            {
                if (chat.UserId1 == userId)
                {
                    totalUnread += chat.UnreadCountUser1;
                }
                else if (chat.UserId2 == userId)
                {
                    totalUnread += chat.UnreadCountUser2;
                }
            }
            return totalUnread;
        }

    }
}