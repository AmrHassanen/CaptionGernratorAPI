using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CaptionGenerator.CORE.Entities
{
    public class EndPoint
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string[] Body { get; set; }

        public string[] WaysToUse { get; set; }

        [Required(ErrorMessage = "Service Id is required.")]
        public int ServiceId { get; set; } // Foreign Key

        // Navigation property for one-to-one relationship with Service
        [JsonIgnore]
        public Service Service { get; set; }
    }
}
