using Newtonsoft.Json;

namespace Reunite.Models
{
    public class Location
    {
        public string Id { get; set; } = null!;
        public string QueryId { get; set; } = null!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Query Query { get; set; } = null!;
    }
}