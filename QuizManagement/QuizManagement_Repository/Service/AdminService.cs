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
        public bool AddAnswer(List<AnswerModel> answerModelList)
        {
            try
            {
                int CheckAnswerAddOrNot = 0;
                int score = 0;
                Result result = new Result();
                
                foreach(var item in answerModelList)
                {
                    Options option = _context.Options.Where(m => m.ques_id == item.ques_id && m.is_correct == true).FirstOrDefault();
                    Answer answer = new Answer
                    {
                        user_id = item.user_id,
                        quiz_id = item.quiz_id,
                        ques_id = item.ques_id,
                        option_id = item.selected_option_id,
                        created_at = DateTime.Now
                    };

                    if(item.selected_option_id == option.option_id)
                    {
                        score += 1;
                    }
                    result.quiz_id = item.quiz_id;
                    result.user_id = item.user_id;


                    _context.Answer.Add(answer);
                    CheckAnswerAddOrNot = _context.SaveChanges();
                    
                }

                result.score = score;
                result.created_at = DateTime.Now;
                _context.Result.Add(result);
                _context.SaveChanges();
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

        public List<QuizModel> AllQuizList()
        {
            try
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
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteQuiz(int quiz_id)
        {
            try
            {
                int checkDelete = 0;
                Quiz quiz = _context.Quiz.Where(q => q.quiz_id == quiz_id).FirstOrDefault();

                Result result = _context.Result.FirstOrDefault(m => m.quiz_id == quiz_id);
                _context.Result.Remove(result);

                List<Question> questionList = new List<Question>();
                foreach (Question question in quiz.Question)
                {
                    Answer answer = _context.Answer.FirstOrDefault(m => m.ques_id == question.ques_id);
                    _context.Answer.Remove(answer);
                    List<Options> optionList = new List<Options>();
                    foreach (Options option in question.Options)
                    {
                        optionList.Add(option);
                    }
                    _context.Options.RemoveRange(optionList);
                    
                    questionList.Add(question);
                }
                _context.Question.RemoveRange(questionList);
                _context.Quiz.Remove(quiz);
                checkDelete = _context.SaveChanges();
                return checkDelete > 0 ? true : false;
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
        public List<QuizModel> GetAllQuizModelList(int userId)
        {
            try
            {
                List<QuizModel> quizModelList = new List<QuizModel>();
                List<Quiz> quizes = _context.Quiz.ToList();
                quizModelList = AdminHelper.ConvertQuizListToQuizModelList(quizes,userId);
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

        public UserModel GetUserProfile(int userId)
        {
            try
            {
                Users user = _context.Users.Where(m => m.user_id == userId).FirstOrDefault();
                UserModel userModel = new UserModel
                {
                    user_id = user.user_id,
                    username = user.username,
                    email = user.email,
                    password = user.password,
                };

                return userModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int QuizScore(int quiz_id, int user_id)
        {
            try
            {
                Result result = _context.Result.Where(m => m.quiz_id == quiz_id && m.user_id == user_id).FirstOrDefault();
                int QuizScore = 0;
                if(result != null)
                {
                    QuizScore = (int)result.score;
                }

                return QuizScore;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateAdminProfile(AdminModel adminModel)
        {
            try
            {
                int checkUpdateOrNot = 0;
                Admin admin = _context.Admin.Where(m => m.admin_id == adminModel.admin_id).FirstOrDefault();
                admin.username = adminModel.username;
                admin.email = adminModel.email;
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

        public bool UpdateUserProfile(UserModel userModel)
        {
            try
            {
                int checkUpdateOrNot = 0;
                Users user = _context.Users.Where(m => m.user_id == userModel.user_id).FirstOrDefault();
                user.username = userModel.username;
                user.email = userModel.email;
                user.password = userModel.password;
                user.updated_at = DateTime.Now;

                _context.Entry(user).State = EntityState.Modified;
                checkUpdateOrNot = _context.SaveChanges();
                return checkUpdateOrNot > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ResultAnswerModel> AllAnswerList(int quiz_id,int user_id)
        {
            try
            {
                List<ResultAnswerModel> list = new List<ResultAnswerModel>();
                /*Quiz quiz = _context.Quiz.FirstOrDefault(m => m.quiz_id == quiz_id);*/
                Quiz quiz = _context.Quiz.Include(q => q.Question).FirstOrDefault(m => m.quiz_id == quiz_id);
                foreach (var item in quiz.Question)
                {
                    ResultAnswerModel resultAnswerModel = new ResultAnswerModel();
                    Question question = item;
                    resultAnswerModel.ques_text = question.ques_text;
                    List<Options> optionList = question.Options.Where(m => m.ques_id == item.ques_id).ToList();
                    List<OptionModel> optionModelList = new List<OptionModel>();

                    foreach(var i in optionList)
                    {
                        OptionModel option = new OptionModel
                        {
                            option_id = i.option_id,
                            is_correct = (bool)i.is_correct,
                            option_text = i.option_text
                        };
                        optionModelList.Add(option);
                    }
                    resultAnswerModel.OptionList = optionModelList;
                    resultAnswerModel.Correct_option_Id = _context.Options.FirstOrDefault(m => m.ques_id == question.ques_id && m.is_correct == true).option_id;
                    int selected_option_id = (int)_context.Answer.FirstOrDefault(m => m.quiz_id == quiz_id && m.ques_id == question.ques_id && m.user_id == user_id).option_id;
                    resultAnswerModel.Selected_option_id = selected_option_id;

                    list.Add(resultAnswerModel);
                }
                return list != null ? list : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
