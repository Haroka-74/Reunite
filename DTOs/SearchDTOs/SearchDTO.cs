using Reunite.Annotations;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Reunite.DTOs.QueryDTOs
{
    public class SearchDTO
    {
        [Required(ErrorMessage = "Image is required")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions([".jpg", ".jpeg", ".png"])]
        public IFormFile Image { get; set; } = null!;

        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; } = null!;

    }
}