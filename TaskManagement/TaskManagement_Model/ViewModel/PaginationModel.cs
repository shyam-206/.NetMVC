using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement_Model.ViewModel
{
    public class PaginationModel
    {
        public List<TaskModel> TaskList { get;  set; }
        public int CurrentIndex { get;  set; }
        public int Count { get;  set; }
    }
}
