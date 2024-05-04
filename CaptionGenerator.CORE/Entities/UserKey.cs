using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CaptionGenerator.CORE.Entities
{

    public class UserKey
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        // Foreign key to ApplicationUser
        [ForeignKey("ApplicationUser")]
        [JsonIgnore]
        public string ApplicationUserId { get; set; }
        [JsonIgnore]
        public ApplicationUser ApplicationUser { get; set; }

        // Foreign key to Key
        [ForeignKey("Key")]
        [JsonIgnore]
        public int KeyId { get; set; }
        [JsonIgnore]
        public Key Key { get; set; }
    }
}
