using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement_Model.ViewModel
{
    public class QuizModel
    {
        public int quiz_id { get; set; }
        [Required(ErrorMessage = "Please Enter a Title of Quiz")]
        public string title { get; set; }
        [Required(ErrorMessage = "Please Enter a Description")]
        public string description { get; set; }
        public Nullable<int> created_By { get; set; }
        [Required(ErrorMessage = "Please ")]
        public List<QuestionModel> QuestionModelList { get; set; }

    }
}
