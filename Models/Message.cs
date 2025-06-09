namespace Reunite.Models
{
    public class Message
    {
        public string Id { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string SenderId { get; set; } = null!;
        public string ChatId { get; set; } = null!;
        public ReuniteUser Sender { get; set; } = null!;
        public Chat Chat { get; set; } = null!;
        public DateTime SentAt { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
    }
}