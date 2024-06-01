using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CourseManagement_Model.ViewModel
{
    public class MaterialModel
    {
        public int MaterialId { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public string[] Files { get; set; }

    }
}
