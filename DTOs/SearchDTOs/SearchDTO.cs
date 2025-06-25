using Reunite.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Reunite.DTOs.SearchDTOs
{
    public class SearchDTO
    {
        [Required(ErrorMessage = "Image is required")]
        [DataType(DataType.Upload)]
        [AllowedExtensions([".jpg", ".jpeg", ".png"], MaxFileSize = 10 * 1024 * 1024)]
        public IFormFile Image { get; set; } = null!;
        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; } = null!;
    }
}