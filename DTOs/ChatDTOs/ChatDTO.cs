namespace Reunite.DTOs.ChatDTOs
{
    public class ChatDTO
    {
        public string ChatId { get; set; } = null!;
        public string ReceiverId { get; set; } = null!;
        public string ReceiverUsername { get; set; } = null!;
        public string LastMessage { get; set; } = null!;
        public DateTime LastMessageTime { get; set; }
        public int UnreadCount { get; set; } = 0;
    }
}