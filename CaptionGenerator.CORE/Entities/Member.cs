using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaptionGenerator.CORE.Entities
{
    public class Member
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Image URL is required.")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Background Image URL is required.")]
        public string? BackgroundImageUrl { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? Email { get; set; }

        // Foreign Key
        public int TeamId { get; set; }

        // Navigation properties
        public Team Team { get; set; }

        // Additional property
        public string[]? Links { get; set; }
    }
}
