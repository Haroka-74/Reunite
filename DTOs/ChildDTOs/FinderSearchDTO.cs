using Microsoft.EntityFrameworkCore;

namespace Reunite.DTOs
{
    public class FinderSearchDTO : SearchDTO
    {
        public LocationAxis Location { get; set; }
    }

    [Owned]
    public class LocationAxis
    {
        public double? latitude { get; set; }
        public double? longitude { get; set; }
    }

}