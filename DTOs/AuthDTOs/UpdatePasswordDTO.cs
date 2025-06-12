using System.ComponentModel.DataAnnotations;

namespace Reunite.DTOs.AuthDTOs
{
    public class UpdatePasswordDTO
    {
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; } = null!;
        [Required(ErrorMessage = "NewPassword is required")]
        public string NewPassword { get; set; } = null!;
    }
}