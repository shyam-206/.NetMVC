using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement_Model.DBContext;

namespace TaskManagement_Model.ViewModel
{
    public class AssignmentModel
    {
        public int AssignmentID { get; set; }
        public string[] StudentID { get; set; }
        public Nullable<int> TaskID { get; set; }
    }
    
}
