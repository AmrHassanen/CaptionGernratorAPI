using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaptionGenerator.CORE.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(50)]
        public string? FirstName { get; set; }

        [Required, MaxLength(50)]
        public string? LastName { get; set; }

        public string? ImageUrl { get; set; }
        public string Key { get; set; } = Guid.NewGuid().ToString();
        public int Limit { get; set; } = 1000;
        public int Usage { get; set; } = 0;
        public DateTime UsageRoundStart { get; set; } = DateTime.Today;
        public List<Service> Services { get; set; }

    }
}
