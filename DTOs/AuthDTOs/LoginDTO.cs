using System.ComponentModel.DataAnnotations;

namespace Reunite.DTOs.AuthDTOs
{
    public class LoginDTO
    {

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username must be at most 50 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}