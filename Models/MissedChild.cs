using System.ComponentModel.DataAnnotations;

namespace Reunite.Models;

public class MissedChild :Child
{
    public string Name { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Age cannot be negative.")]
    public int Age { get; set; }
}