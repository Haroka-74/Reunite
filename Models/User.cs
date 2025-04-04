namespace Reunite.Models;

public class User
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Location { get; set; }
    public ICollection<FoundChild> FoundChilds { get; set; } = [];
    public ICollection<MissedChild> MissedChilds { get; set; } = [];

}