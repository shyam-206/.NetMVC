using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement_Model.ViewModel
{
    public class OptionModel
    {
        public int option_id { get; set; }
        public Nullable<int> ques_id { get; set; }
        public string option_text { get; set; }
        public bool is_correct { get; set; }
    }
}
