using QuizManagement.Session;
using QuizManagement_Model.ViewModel;
using QuizManagement_Repository.Interface;
using QuizManagement_Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdminRespository repository;

        public HomeController()
        {
            repository = new AdminService();
        }
        public ActionResult Index()
        {
            ViewBag.Email = SessionHelper.Useremail;
            int userId = SessionHelper.UserId;
            ViewBag.UserId = SessionHelper.UserId; 
            List<QuizModel> quizModelList = repository.GetAllQuizModelList(userId);
            return View(quizModelList);
        }

        public ActionResult QuizStart(int quiz_id)
        {
        
            QuizModel quizModel = repository.GetQuizById(quiz_id);
            return View(quizModel);
        }

        [HttpGet]
        public PartialViewResult GetQuestion(int quiz_id)
        {
            try
            {
                QuizModel quizModel = repository.GetQuizById(quiz_id);
                QuestionModel questionModel = quizModel.QuestionModelList.OrderBy(m => m.ques_id).FirstOrDefault();
                QuestionModel currentQuestionModel = new QuestionModel
                {
                    quiz_id = quiz_id,
                    ques_id = questionModel.ques_id,
                    ques_text = questionModel.ques_text,
                    OptionList = questionModel.OptionList.Select(m => new OptionModel
                    {
                        option_id = m.option_id,
                        option_text = m.option_text,
                        is_correct = m.is_correct
                    }).ToList()
                };
                return PartialView("_QuestionPartial", currentQuestionModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult GetNextQuestion(int quiz_id, int currentQuestionId)
        {
            try
            {
                QuizModel quizModel = repository.GetQuizById(quiz_id);
                var nextQuestion = quizModel.QuestionModelList
                    .Where(q => q.ques_id > currentQuestionId)
                    .OrderBy(q => q.ques_id)
                    .FirstOrDefault();
                var questionModel = new QuestionModel
                {
                    quiz_id = quiz_id,
                    ques_id = nextQuestion.ques_id,
                    ques_text = nextQuestion.ques_text,
                    OptionList = nextQuestion.OptionList.Select(m => new OptionModel
                    {
                        option_id = m.option_id,
                        option_text = m.option_text,
                        is_correct = m.is_correct
                    }).ToList()
                };

                return PartialView("_QuestionPartial", questionModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult SaveAnswer(List<AnswerModel> answerModelList)
        {
            try
            {
                bool CheckSaveAnswerList = repository.AddAnswer(answerModelList);

                if (CheckSaveAnswerList)
                {
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
                }
                return RedirectToAction("QuizStart");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult ViewResult(int quiz_id,int user_id)
        {
            try
            {
                ViewBag.score = repository.QuizScore(quiz_id, user_id);
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ActionResult GetPreviousQuestion(int quiz_id, int currentQuestionId)
        {
            try
            {
                QuizModel quizModel = repository.GetQuizById(quiz_id);
                var nextQuestion = quizModel.QuestionModelList
                    .Where(q => q.ques_id < currentQuestionId)
                    .OrderByDescending(q => q.ques_id)
                    .FirstOrDefault();
                QuestionModel questionModel = new QuestionModel
                {
                    quiz_id = quiz_id,
                    ques_id = nextQuestion.ques_id,
                    ques_text = nextQuestion.ques_text,
                    OptionList = nextQuestion.OptionList.Select(m => new OptionModel
                    {
                        option_id = m.option_id,
                        option_text = m.option_text,
                        is_correct = m.is_correct
                    }).ToList()
                };

                return PartialView("_QuestionPartial", questionModel);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ActionResult UserProfile(int userId)
        {
            try
            {
                UserModel userModel = repository.GetUserProfile(userId);
                return View(userModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public ActionResult UserProfile(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                bool checkUpdateProflie = repository.UpdateUserProfile(userModel);

                if (checkUpdateProflie)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(userModel);
                }
            }
            else
            {
                return View(userModel);
            }
            
        }
    }
}