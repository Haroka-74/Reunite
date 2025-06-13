namespace Reunite.Models
{
    public class ReuniteUser
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FcmToken { get; set; }
        public ICollection<Query> Queries { get; set; } = [];
        public ICollection<Chat> SentChats { get; set; } = [];
        public ICollection<Chat> ReceivedChats { get; set; } = [];
        public ICollection<Message> SentMessages { get; set; } = [];
    }
}