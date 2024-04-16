using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CaptionGenerator.CORE.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Add the foreign key property here
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        [JsonIgnore]
        public Team Team { get; set; }

        [Required(ErrorMessage = "Image URL is required.")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Background Image URL is required.")]
        public string? BackgroundImageUrl { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? Email { get; set; }

        public string Description { get; set; }
        // Additional property
        public string[]? Links { get; set; }
    }
}
