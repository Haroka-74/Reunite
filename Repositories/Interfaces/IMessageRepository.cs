using Reunite.Models;

namespace Reunite.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetMessagesAsync();
        Task<Message> GetMessageAsync(string messageId);
        Task AddMessageAsync(Message message);
    }
}