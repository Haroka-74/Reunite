namespace Reunite.Models;

public class Child
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}