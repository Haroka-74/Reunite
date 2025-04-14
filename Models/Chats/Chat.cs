
using Reunite.Models.Auth;

namespace Reunite.Models.Chats
{
    public class Chat
    {
        public string Id { get; set; } = null!;
        public string UserId1 { get; set; } = null!;
        public ReuniteUser User1 { get; set; } = null!;
        public string UserId2 { get; set; } = null!;
        public ReuniteUser User2 { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Message> Messages { get; set; } = [];
    }
}