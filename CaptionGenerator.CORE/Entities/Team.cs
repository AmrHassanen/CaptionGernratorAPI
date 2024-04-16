using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using CaptionGenerator.CORE.Interfaces;

namespace CaptionGenerator.CORE.Entities
{
    public class Team
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string BackgroundImageUrl { get; set; }

        // Navigation property to Members (One-to-Many)
        [JsonIgnore]
        public List<Member> Members { get; set; }

        // Foreign key for Service
        [ForeignKey("Service")]
        public int ServiceId { get; set; }

        // Navigation property for Service
        [JsonIgnore]
        public Service Service { get; set; }
    }
}
