using System.ComponentModel.DataAnnotations;

namespace Reunite.Models.Children;

public class MissedChild : Child
{

    public string Name { get; set; } = null!;

    [Range(0, int.MaxValue, ErrorMessage = "Age cannot be negative.")]
    public int Age { get; set; }

}