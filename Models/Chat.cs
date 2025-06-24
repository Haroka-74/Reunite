namespace Reunite.Models
{
    public class Chat
    {
        public string Id { get; set; } = null!;
        public string UserId1 { get; set; } = null!;
        public string UserId2 { get; set; } = null!;
        public ReuniteUser User1 { get; set; } = null!;
        public ReuniteUser User2 { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Message> Messages { get; set; } = [];
        public int UnreadCountUser1 { get; set; } = 0;
        public int UnreadCountUser2 { get; set; } = 0;
    }
}