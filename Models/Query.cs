namespace Reunite.Models;

public class Query
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public ReuniteUser User { get; set; } = null!;
    public int? Age { get; set; }
    public Location? Location { get; set; } = null!;
    public FacebookPost FacebookPost { get; set; } = null!;

    public bool isParent { get; set; }
}