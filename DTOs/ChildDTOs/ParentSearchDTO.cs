using System.ComponentModel.DataAnnotations;

namespace Reunite.DTOs
{
    public class ParentSearchDTO : SearchDTO
    {
        [Required]
        [MaxLength(100)]
        public string ChildName { get; set; } = null!;
        [Range(0, 15, ErrorMessage = "Not Valid Age")]
        [Required]
        public int ChildAge { get; set; }
    }
}