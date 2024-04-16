using CaptionGenerator.CORE.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CaptionGenerator.CORE.Entities
{
    public class Service
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Number of Requests is required.")]
        public int NumberOfRequests { get; set; }

        [Required(ErrorMessage = "Image URL is required.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Background Image URL is required.")]
        public string BackgroundImageUrl { get; set; }

        // Navigation property for one-to-many relationship with EndPoint
        public ICollection<EndPoint> EndPoints { get; set; }

        // Navigation property for one-to-many relationship with Team
        public ICollection<Team> Teams { get; set; }
        
    }
}
