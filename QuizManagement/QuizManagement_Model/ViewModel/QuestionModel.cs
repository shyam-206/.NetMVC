using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement_Model.ViewModel
{
    public class QuestionModel
    {
        public int ques_id { get; set; }
        public Nullable<int> quiz_id { get; set; }
        public string ques_text { get; set; }
        public List<OptionModel> OptionList { get; set; }
    }
}
