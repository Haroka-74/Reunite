using Reunite.Models.Children;

namespace Reunite.Models.Auth
{
    public class ReuniteUser
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ICollection<FoundChild> FoundChilds { get; set; } = [];
        public ICollection<MissedChild> MissedChilds { get; set; } = [];
    }
}