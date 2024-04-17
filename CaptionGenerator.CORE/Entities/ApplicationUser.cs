using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CaptionGenerator.CORE.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? ImageUrl { get; set; }

        // Navigation property to UserKey join entity
        [JsonIgnore]
        public ICollection<UserKey> UserKeys { get; set; }

        // Define other properties as needed
        public int Limit { get; set; } = 1000;
        public int Usage { get; set; } = 0;
    }
}
