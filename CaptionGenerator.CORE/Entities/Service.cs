using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaptionGenerator.CORE.Entities
{
    public class Service
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Team Id is required.")]
        public int TeamId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Number of Requests is required.")]
        public int NumberOfRequests { get; set; }

        [Required(ErrorMessage = "Image URL is required.")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Background Image URL is required.")]
        public string? BackgroundImageUrl { get; set; }

        // Foreign Key
        public int EndPointId { get; set; } // EndPointId added

        // Navigation properties
        public Team Team { get; set; }
        public List<ApplicationUser> Users { get; set; } // Many-to-Many with users
        public EndPoint EndPoint { get; set; } // One-to-One with endpoint
    }
}
