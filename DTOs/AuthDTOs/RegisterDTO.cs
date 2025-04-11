using System.ComponentModel.DataAnnotations;

namespace Reunite.DTOs.AuthDTOs
{
    public class RegisterDTO
    {     
        public string Auth0Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
        
    }
}