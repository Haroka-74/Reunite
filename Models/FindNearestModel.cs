using System.Text.Json.Serialization;

namespace Reunite.Models
{
    public class FindNearestModel
    {
        public string? Message { get; set; }
        public string? Id { get; set; }
        public string? FromParent { get; set; }
        public string? Date { get; set; }
        public string? Image { get; set; }
        [JsonIgnore]
        public int StautsCode { get; set; }
    }
}
