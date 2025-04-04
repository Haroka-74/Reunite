using System.ComponentModel.DataAnnotations;

namespace Reunite.DTOs.AuthDTOs
{
    public class RefreshTokenDTO
    {

        [Required(ErrorMessage = "Refresh token is required.")]
        public string RefreshToken { get; set; }

    }
}