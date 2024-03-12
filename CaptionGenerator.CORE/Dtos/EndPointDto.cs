using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptionGenerator.CORE.Dtos
{
    public class EndPointDto
    {
        public string? Url { get; set; }

        public string[]? Body { get; set; }

        public string[]? WaysToUse { get; set; }
        // Foreign Key
        public int ServiceId { get; set; }
    }
}
