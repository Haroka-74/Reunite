using Reunite.Models;

namespace Reunite.DTOs
{
    public class FinderSearchDTO : SearchDTO
    {
        public Location Location { get; set; } = null!;
    }

}