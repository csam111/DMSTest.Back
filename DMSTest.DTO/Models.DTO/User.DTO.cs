using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSTest.DTO.Models.DTO
{
    public class User
    {
    
        public int IdUsers { get; set; }

        public string? Nombre { get; set; }

        public string? Email { get; set; }
    
        public string? Password { get; set; }
    }
}
