using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptionGenerator.CORE.Dtos
{
    public class ServiceDto
    {
        [Required(ErrorMessage = "Team Id is required.")]
        public int TeamId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Number of Requests is required.")]
        public int NumberOfRequests { get; set; }

        [Required(ErrorMessage = "URL is required.")]
        public string? Url { get; set; } // Represents the endpoint URL

        [Required(ErrorMessage = "Image URL is required.")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Background Image URL is required.")]
        public string? BackgroundImageUrl { get; set; }
    }
}
