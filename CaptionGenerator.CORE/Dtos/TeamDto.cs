using CaptionGenerator.CORE.Attributes;
using CaptionGenerator.CORE.Entities;
using CaptionGenerator.CORE.Settings;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptionGenerator.CORE.Dtos
{
    public class TeamDto
    {
        [Required(ErrorMessage = "Image URL is required.")]
        [AllowedExtension(FileSettings.AllowedExtensions),
                    maxSizeAllowed(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? ImageUrl { get; set; }

        [Required(ErrorMessage = "Background Image URL is required.")]
        [AllowedExtension(FileSettings.AllowedExtensions),
            maxSizeAllowed(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? BackgroundImageUrl { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int ServiceId { get; set; }
    }
}
