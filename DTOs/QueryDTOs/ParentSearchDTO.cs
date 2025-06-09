using System.ComponentModel.DataAnnotations;

namespace Reunite.DTOs.QueryDTOs
{
    public class ParentSearchDTO : SearchDTO
    {
        [Required(ErrorMessage = "Child name is required")]
        [MaxLength(100, ErrorMessage = "Child name cannot exceed 100 characters")]
        public string ChildName { get; set; } = null!;
        [Required(ErrorMessage = "Child age is required")]
        [Range(1, 15, ErrorMessage = "Child age must be between 0 and 15")]
        public int ChildAge { get; set; }
    }
}