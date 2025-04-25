using Reunite.DTOs;
using Reunite.Models.Auth;

namespace Reunite.Models.Children;

public class Child
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public ReuniteUser User { get; set; } = null!;
    public int? Age { get; set; }
    public LocationAxis? Location { get; set; } = null!;

}