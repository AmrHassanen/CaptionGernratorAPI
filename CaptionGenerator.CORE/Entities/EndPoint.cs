using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptionGenerator.CORE.Entities
{
    public class EndPoint
    {
        public int Id { get; set; } 

        public string? Url { get; set; }

        public string[]? Body { get; set; }

        public string[]? WaysToUse { get; set; }
        // Foreign Key
        public int ServiceId { get; set; }

        // Navigation property
        public Service Service { get; set; } // One-to-One with service
    }
}