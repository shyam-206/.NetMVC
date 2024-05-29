using Newtonsoft.Json;
using QuizManagement.Common;
using QuizManagement.CustomFilter;
using QuizManagement.Session;
using QuizManagement_Model.ViewModel;
using QuizManagement_Repository.Interface;
using QuizManagement_Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QuizManagement.Controllers
{
    [CustomAuthorizeFilter]
    public class HomeController : Controller
    {
        private readonly IAdminRespository repository;

        public HomeController()
        {
            repository = new AdminService();
        }
        public async Task<ActionResult> Index()
        {
            ViewBag.Email = SessionHelper.Useremail;
            int userId = SessionHelper.UserId;
            ViewBag.UserId = SessionHelper.UserId;
            /*List<QuizModel> quizModelList = repository.GetAllQuizModelList(userId);*/

            string url = $"api/UserAPI/GetAllQuizModelList?userId={userId}";
            string res = await WebHelper.HttpRequestResponse(url);
            List<QuizModel> quizModelList = JsonConvert.DeserializeObject<List<QuizModel>>(res);

            return View(quizModelList);
        }
        public async Task<ActionResult> QuizStart(int quiz_id)
        {
            try
            {
                /*QuizModel quizModel = repository.GetQuizById(quiz_id);*/
                string url = $"api/UserAPI/GetQuizById?quiz_id={quiz_id}";
                string res = await WebHelper.HttpRequestResponse(url);
                QuizModel quizModel = JsonConvert.DeserializeObject<QuizModel>(res);
                return View(quizModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        public async Task<PartialViewResult> GetQuestion(int quiz_id)
        {
            try
            {
                /*QuizModel quizModel = repository.GetQuizById(quiz_id);*/

                string url = $"api/UserAPI/GetQuizById?quiz_id={quiz_id}";
                string res = await WebHelper.HttpRequestResponse(url);
                QuizModel quizModel = JsonConvert.DeserializeObject<QuizModel>(res);
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
        public async Task<ActionResult> GetNextQuestion(int quiz_id, int currentQuestionId)
        {
            try
            {
                /*QuizModel quizModel = repository.GetQuizById(quiz_id);*/
                string url = $"api/UserAPI/GetQuizById?quiz_id={quiz_id}";
                string res = await WebHelper.HttpRequestResponse(url);
                QuizModel quizModel = JsonConvert.DeserializeObject<QuizModel>(res);
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
        public async Task<ActionResult> GetPreviousQuestion(int quiz_id, int currentQuestionId)
        {
            try
            {
                /*QuizModel quizModel = repository.GetQuizById(quiz_id);*/
                string url = $"api/UserAPI/GetQuizById?quiz_id={quiz_id}";
                string res = await WebHelper.HttpRequestResponse(url);
                QuizModel quizModel = JsonConvert.DeserializeObject<QuizModel>(res);
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
        [HttpPost]
        public async Task<ActionResult> SaveAnswer(List<AnswerModel> answerModelList)
        {
            try
            {
                /*bool CheckSaveAnswerList = repository.AddAnswer(answerModelList);*/
                string url = "api/UserAPI/AddAnswer";
                string content = JsonConvert.SerializeObject(answerModelList);
                string res = await WebHelper.HttpClientPostRequest(url, content);
                bool CheckSaveAnswerList = JsonConvert.DeserializeObject<bool>(res);

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
        public async Task<ActionResult> ViewResult(int quiz_id,int user_id)
        {
            try
            {
                /*ViewBag.score = repository.QuizScore(quiz_id, user_id);*/
                string url = $"api/UserAPI/QuizScore?quiz_id={quiz_id}&user_id={user_id}";
                string res = await WebHelper.HttpRequestResponse(url);
                ViewBag.score = JsonConvert.DeserializeObject<int>(res);
                List<ResultAnswerModel> list = repository.AllAnswerList(quiz_id,user_id);
                return View(list);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<ActionResult> UserProfile(int userId)
        {
            try
            {
                /*UserModel userModel = repository.GetUserProfile(userId);*/
                string url = $"api/UserAPI/GetUserProfile?userId={userId}";
                string res = await WebHelper.HttpRequestResponse(url);
                UserModel userModel = JsonConvert.DeserializeObject<UserModel>(res);
                return View(userModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        public async Task<ActionResult> UserProfile(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                /*bool checkUpdateProflie = repository.UpdateUserProfile(userModel);*/
                string url = "api/UserAPI/UpdateUserProfile";
                string content = JsonConvert.SerializeObject(userModel);
                string res = await WebHelper.HttpClientPostRequest(url, content);
                bool checkUpdateProflie = JsonConvert.DeserializeObject<bool>(res);

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