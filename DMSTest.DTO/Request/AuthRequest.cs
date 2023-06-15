using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSTest.DTO.Request
{
    public class AuthRequest
    {
        [Required]
        public string User { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
