using QuizManagement_Helper.ModelHelper;
using QuizManagement_Model.DbContext;
using QuizManagement_Model.ViewModel;
using QuizManagement_Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement_Repository.Service
{
    public class AdminService : IAdminRespository
    {
        private readonly Quizmanagement_557Entities _context;
        public AdminService()
        {
            _context = new Quizmanagement_557Entities();
        }
        public bool AddQuiz(QuizModel quizModel)
        {
            try
            {
                int CheckQuizSave = 0;
                Quiz quiz = AdminHelper.ConvertQuizToQuizModel(quizModel);
                _context.Quiz.Add(quiz);
                _context.SaveChanges();


                for(var i = 0; i < quizModel.QuestionModelList.Count(); i++)
                {
                    Question question = new Question {
                        quiz_id = quiz.quiz_id,
                        ques_text = quizModel.QuestionModelList[i].ques_text,
                        created_at = DateTime.Now
                    };

                    _context.Question.Add(question);
                    _context.SaveChanges();

                    foreach(var item in quizModel.QuestionModelList[i].OptionList)
                    {
                        Options option = new Options
                        {
                            ques_id = question.ques_id,
                            option_text = item.option_text,
                            is_correct = item.is_correct,
                            created_at = DateTime.Now
                        };

                        _context.Options.Add(option);
                        CheckQuizSave =  _context.SaveChanges();
                    }
                }

             

                return CheckQuizSave > 0 ? true : false;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<QuizModel> GetAllQuizModelList()
        {   
            try
            {
                List<QuizModel> quizModelList = new List<QuizModel>();
                List<Quiz> quizes = _context.Quiz.ToList();
                quizModelList = AdminHelper.ConvertQuizListToQuizModelList(quizes);
                return quizModelList != null ? quizModelList : null;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
