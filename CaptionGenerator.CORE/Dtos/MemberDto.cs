using CaptionGenerator.CORE.Attributes;
using CaptionGenerator.CORE.Settings;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CaptionGenerator.CORE.Dtos
{
    public class MemberDto
    {
        [Required(ErrorMessage = "Image URL is required.")]
        [AllowedExtension(FileSettings.AllowedExtensions),
            maxSizeAllowed(FileSettings.MaxFileSizeInBytes)]
        [JsonIgnore]
        public IFormFile? ImageUrl { get; set; }

        [Required(ErrorMessage = "Background Image URL is required.")]
        [AllowedExtension(FileSettings.AllowedExtensions),
            maxSizeAllowed(FileSettings.MaxFileSizeInBytes)]
        [JsonIgnore]
        public IFormFile? BackgroundImageUrl { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? Email { get; set; }

        // Foreign Key
        public int TeamId { get; set; }
    }
}
