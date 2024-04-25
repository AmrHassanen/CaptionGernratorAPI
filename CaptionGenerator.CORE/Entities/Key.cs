using System.Collections.Generic;

namespace CaptionGenerator.CORE.Entities
{
    public class Key
    {
        public int Id { get; set; }

        // Navigation property to UserKey join entity
        public ICollection<UserKey> UserKeys { get; set; }
        public Guid KeyValue { get; set; }
        public int Limit { get; set; } = -1;
        public int Usage { get; set; } = 0;
        public int RateLimit { get; set; } = -1;
    }
}
