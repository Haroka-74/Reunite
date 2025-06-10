namespace Reunite.Models
{
    public class Query
    {
        public string Id { get; set; } = null!;
        public string ChildImage { get; set; } = null!;
        public string? ChildName { get; set; } = null!;
        public int? ChildAge { get; set; }
        public bool IsParent { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string UserId { get; set; } = null!;
        public Location? Location { get; set; } = null!;
        public ReuniteUser User { get; set; } = null!;
        public FacebookPost FacebookPost { get; set; } = null!;
    }
}