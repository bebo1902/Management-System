using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management.models
{
    public class Task_User
    {
        [Key]
        public int UserID { get; set; }
        public string Password { get; set; }
        public string Name_user { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
