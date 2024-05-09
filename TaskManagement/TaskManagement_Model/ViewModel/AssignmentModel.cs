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
    
}
