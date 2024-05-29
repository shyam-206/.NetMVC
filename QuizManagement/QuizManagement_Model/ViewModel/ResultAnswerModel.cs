using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement_Model.ViewModel
{
    public class ResultAnswerModel
    {
        public string ques_text { get; set; }
        public List<OptionModel> OptionList { get; set; }
        public int Correct_option_Id { get; set; }

        public int Selected_option_id { get; set; }
    }
}
