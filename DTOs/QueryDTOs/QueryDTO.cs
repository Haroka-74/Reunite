namespace Reunite.DTOs.QueryDTOs;

public class QueryDTO
{
    public string Id { get; set; } = null!;
    public string ChildImage { get; set; } = null!;
    public string? ChildName { get; set; } = null!;
    public int? ChildAge { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

}