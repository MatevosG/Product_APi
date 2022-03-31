using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class UserDto
    {
        public int Id { get; set; } 
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; } 
    }
}
