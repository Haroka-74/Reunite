namespace Reunite.DTOs
{
    public class SearchDTO
    {
        public IFormFile Image { get; set; }
        public bool IsParent { get; set; }
        public string UserId { get; set; } 
    }
}