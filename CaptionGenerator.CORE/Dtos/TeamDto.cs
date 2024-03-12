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

        [Required(ErrorMessage = "Member Ids list is required.")]
        public List<int> MemberIds { get; set; } = new List<int>();

        [Required(ErrorMessage = "Image URL is required.")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Background Image URL is required.")]
        public string? BackgroundImageUrl { get; set; }
    }
}
