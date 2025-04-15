using Microsoft.EntityFrameworkCore;
using Reunite.Data;
using Reunite.Models.Chats;
using Reunite.Repositories.Interfaces;

namespace Reunite.Repositories.Implementations
{
    public class ChatRepository : IChatRepository
    {

        private readonly ReuniteDbContext context = null!;

        public ChatRepository(ReuniteDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Chat>> GetChatsAsync()
        {
            return await context.Chats.Include(c => c.User1).Include(c => c.User2).Include(c => c.Messages).ToListAsync();
        }

        public async Task<Chat> GetChatAsync(string chatId)
        {
            return await context.Chats.Include(c => c.User1).Include(c => c.User2).Include(c => c.Messages).FirstOrDefaultAsync(c => c.Id == chatId);
        }

        public async Task CreateChatAsync(Chat chat)
        {
            await context.Chats.AddAsync(chat);
            await context.SaveChangesAsync();
        }

    }
}