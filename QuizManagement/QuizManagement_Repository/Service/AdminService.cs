using QuizManagement_Helper.ModelHelper;
using QuizManagement_Model.DbContext;
using QuizManagement_Model.ViewModel;
using QuizManagement_Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public bool AddAnswer(AnswerModel answerModel)
        {
            try
            {
                int CheckAnswerAddOrNot = 0;
                Answer answer = new Answer {
                    user_id = answerModel.user_id,
                    quiz_id = answerModel.quiz_id,
                    ques_id = answerModel.ques_id,
                    option_id = answerModel.selected_option_id,
                    created_at = DateTime.Now
                };

                _context.Answer.Add(answer);
                CheckAnswerAddOrNot = _context.SaveChanges();

                return CheckAnswerAddOrNot > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool AddQuiz(QuizModel quizModel)
        {
            try
            {
                int CheckQuizSave = 0;
                Quiz quiz = AdminHelper.ConvertQuizToQuizModel(quizModel);
                _context.Quiz.Add(quiz);
                _context.SaveChanges();


                for (var i = 0; i < quizModel.QuestionModelList.Count(); i++)
                {
                    Question question = new Question
                    {
                        quiz_id = quiz.quiz_id,
                        ques_text = quizModel.QuestionModelList[i].ques_text,
                        created_at = DateTime.Now
                    };

                    _context.Question.Add(question);
                    _context.SaveChanges();

                    foreach (var item in quizModel.QuestionModelList[i].OptionList)
                    {
                        Options option = new Options
                        {
                            ques_id = question.ques_id,
                            option_text = item.option_text,
                            is_correct = item.is_correct,
                            created_at = DateTime.Now
                        };

                        _context.Options.Add(option);
                        CheckQuizSave = _context.SaveChanges();
                    }
                }



                return CheckQuizSave > 0 ? true : false;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public AdminModel GetAdminProfile(int adminId)
        {
            try
            {
                Admin admin = _context.Admin.Where(m => m.admin_id == adminId).FirstOrDefault();
                AdminModel adminModel = LoginHelper.ConvertAdminToAdminModel(admin);
                return adminModel != null ? adminModel : null;
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

        public QuizModel GetQuizById(int quiz_id)
        {
            try
            {

                Quiz quiz = _context.Quiz.Where(m => m.quiz_id == quiz_id).FirstOrDefault();
                QuizModel quizModel = AdminHelper.ConvertQuizToQuizModel(quiz);
                return quizModel != null ? quizModel : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool UpdateAdminProfile(AdminModel adminModel)
        {
            try
            {
                int checkUpdateOrNot = 0;
                Admin admin = _context.Admin.Where(m => m.admin_id == adminModel.admin_id).FirstOrDefault();
                admin.username = adminModel.username;
                adminModel.email = adminModel.email;
                admin.password = adminModel.password;
                admin.updated_at = DateTime.Now;

                _context.Entry(admin).State = EntityState.Modified;
                checkUpdateOrNot = _context.SaveChanges();
                return checkUpdateOrNot > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool UpdateQuizById(QuizModel quizModel)
        {
            try
            {
                int checkQuizUpdateOrNot = 0;
                
                Quiz quiz = _context.Quiz.Where(m => m.quiz_id == quizModel.quiz_id).FirstOrDefault();
                quiz.title = quizModel.title;
                quiz.description = quizModel.description;
                quiz.updated_at = DateTime.Now;
                _context.Entry(quiz).State = EntityState.Modified;
                checkQuizUpdateOrNot = _context.SaveChanges(); 

                for (var i = 0; i < quizModel.QuestionModelList.Count(); i++)
                {
                    //Question are update
                    Question question = quiz.Question.Where(m => m.ques_id == quizModel.QuestionModelList[i].ques_id).FirstOrDefault();
                    question.ques_text = quizModel.QuestionModelList[i].ques_text;
                    question.updated_at = DateTime.Now;

                    //after that save changes in the database
                    _context.Entry(question).State = EntityState.Modified;
                    _context.SaveChanges();

                    foreach(var item in quizModel.QuestionModelList[i].OptionList)
                    {
                        Options option = question.Options.Where(m => m.option_id == item.option_id).FirstOrDefault();

                        option.option_text = item.option_text;
                        option.is_correct = item.is_correct;
                        option.updated_at = DateTime.Now;
                        _context.Entry(option).State = EntityState.Modified;
                        _context.SaveChanges();

                    }
                }
                return checkQuizUpdateOrNot > 0 ? true  : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
