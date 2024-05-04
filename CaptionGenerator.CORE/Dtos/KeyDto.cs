using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptionGenerator.CORE.Dtos
{
    public class KeyDto
    {
        public int Limit { get; set; }
        //public int Usage { get; set; }
        //public int RateLimit { get; set; }
    }

    public class NewKeyDto
    {
        public string KeyValue { get; set; }
        public int Limit { get; set; }
        public int Usage { get; set; }
        public int RateLimit { get; set; }
    }

    public class KeyUpdateRequest
    {
        public int Usage { get; set; }
        public int Limit { get; set; }
        public int RateLimit { get; set; }
    }
}
