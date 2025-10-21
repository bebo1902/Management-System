using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management.models
{
    public class Tasks
    {
        [Key]
        public int TaskID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int id_user { get; set; }
        [ForeignKey ("id_user")]
        public virtual Task_User User { get; set; }
       
    }
}
