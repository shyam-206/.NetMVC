using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement_Model.DBContext;

namespace TaskManagement_Model.ViewModel
{
    public class TaskModel
    {
        public int TaskID { get; set; }
        [Required(ErrorMessage = "Please Give Task name")]
        [MinLength(5,ErrorMessage = "Please provide a task name length 5")]
        [MaxLength(15, ErrorMessage = "Task name length is not more than 15")]
        public string TaskName { get; set; }
        [Required(ErrorMessage = "Please provide a description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please provide a deadline")]
        public Nullable<System.DateTime> Deadline { get; set; }
        public Nullable<int> CreatorID { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
