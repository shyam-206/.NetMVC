using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement_Model.ViewModel
{
    public class AnswerModel
    {
        public int answer_id { get; set; }
        public int user_id { get; set; }
        public int quiz_id { get; set; }
        public int ques_id { get; set; }
        public int selected_option_id { get; set; }
    }
}
