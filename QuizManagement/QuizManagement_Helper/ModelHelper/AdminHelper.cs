using QuizManagement_Model.DbContext;
using QuizManagement_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement_Helper.ModelHelper
{
    public class AdminHelper
    {
        public static Quiz ConvertQuizToQuizModel(QuizModel quizModel)
        {
            Quiz quiz = new Quiz();
            quiz.quiz_id = quizModel.quiz_id;
            quiz.title = quizModel.title;
            quiz.description = quizModel.description;
            quiz.created_By = quizModel.created_By;
            quiz.created_at = DateTime.Now;
            return quiz;
        }

        public static List<QuizModel> ConvertQuizListToQuizModelList(List<Quiz> quizes)
        {
            try
            {
                List<QuizModel> quizModelList = new List<QuizModel>();
                if(quizes != null)
                {
                    foreach(Quiz quiz in quizes)
                    {
                        QuizModel quizModel = new QuizModel();
                        quizModel.quiz_id = quiz.quiz_id;
                        quizModel.title = quiz.title;
                        quizModel.description = quiz.description;
                        quizModel.created_By = quiz.created_By;
                        quizModelList.Add(quizModel);
                    }
                }
                return quizModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
