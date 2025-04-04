using System.ComponentModel.DataAnnotations;

namespace Reunite.DTOs.AuthDTOs;

public class ForgetPasswordDTO
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(128, ErrorMessage = "Email must be at most 128 characters")]
    public string Email { get; set; }
}