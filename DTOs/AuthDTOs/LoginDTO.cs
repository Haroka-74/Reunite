using System.ComponentModel.DataAnnotations;

namespace Reunite.DTOs.AuthDTOs
{
    public class LoginDTO
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(128, ErrorMessage = "Email must be at most 128 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}