using Reunite.Models.Chats;
using Reunite.Models.Children;

namespace Reunite.Models.Auth
{
    public class ReuniteUser
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ICollection<Child> Childs { get; set; } = [];
        public ICollection<Chat> SentChats { get; set; } = [];
        public ICollection<Chat> ReceivedChats { get; set; } = [];
        public ICollection<Message> SentMessages { get; set; } = [];
    }
}