using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaptionGenerator.CORE.Entities
{
    public class Team
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Member Ids list is required.")]
        public List<int> MemberIds { get; set; } = new List<int>();

        [Required(ErrorMessage = "Image URL is required.")]
        public string ?ImageUrl { get; set; }

        [Required(ErrorMessage = "Background Image URL is required.")]
        public string ?BackgroundImageUrl { get; set; }

        public List<Service>? Services { get; set; }
        public List<Member> ?Members { get; set; }
    }
}
