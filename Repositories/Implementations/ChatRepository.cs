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

    }
}