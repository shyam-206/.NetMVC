using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TaskManagement_Model.DBContext;

namespace TaskManagement_Model.ViewModel
{
    public class AssignmentModel
    {
        public int AssignmentID { get; set; }

        [Required(ErrorMessage = "Please select student")]
        public string[] StudentID { get; set; }

        [Required(ErrorMessage = "Please select Task")]
        public Nullable<int> TaskID { get; set; }
        public Nullable<bool> Status { get; set; }
    }  
    
    public class AssignmentModelList
    {
        public int AssignmentID { get; set; }     
        public int StudentID { get; set; }
        public Nullable<int> TaskID { get; set; }
        public Nullable<bool> Status { get; set; }
        public string StudentName { get; set; }
        public string TaskName { get; set; }

        public string TeacherName { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
    }
    
}
