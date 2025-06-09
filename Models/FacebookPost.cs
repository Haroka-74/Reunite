namespace Reunite.Models
{
    public class FacebookPost
    {
        public string Id { get; set; } = null!;
        public string QueryId { get; set; } = null!;
        public string Link { get; set; } = null!;
        public Query Query { get; set; } = null!;
    }
}