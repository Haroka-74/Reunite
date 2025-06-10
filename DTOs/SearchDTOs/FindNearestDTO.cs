using Reunite.Models;

namespace Reunite.DTOs.QueryDTOs
{
    public class FindNearestDTO
    {
        public string Id { get; set; } = null!;
        public bool IsParent { get; set; }
        public string Date { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string ChatId { get; set; } = null!;
        public string ReceiverId { get; set; } = null!;
        public string ReceiverUsername { get; set; } = null!;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}