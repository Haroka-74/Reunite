using System.ComponentModel.DataAnnotations;

namespace Reunite.DTOs
{
    public class SearchDTO
    {
        [Required]
        public IFormFile Image { get; set; } = null!;
        [Required]
        public bool IsParent { get; set; }
        [MaxLength(100)]
        public string UserId { get; set; } = null!;
    }
}