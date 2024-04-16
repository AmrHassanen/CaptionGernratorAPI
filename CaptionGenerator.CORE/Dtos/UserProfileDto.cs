using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptionGenerator.CORE.Dtos
{
    public class UserProfileDto
    {
        public string ImageUrl { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int Limit { get; set; }
        public int Usage { get; set; }
    }

}
