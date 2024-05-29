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
    public class AdminController : Controller
    {
        private readonly IAdminRespository repository;

        public AdminController()
        {
            repository = new AdminService();
        }
        public async Task<ActionResult> Index()
        {
            ViewBag.adminId = SessionHelper.UserId;
            /*List<QuizModel> quizModelList = repository.AllQuizList();*/
            string url = $"api/AdminAPI/AllQuizList";
            string res = await WebHelper.HttpRequestResponse(url);
            List<QuizModel> quizModelList = JsonConvert.DeserializeObject<List<QuizModel>>(res);

            return View(quizModelList);
        }

        public ActionResult Create()
        { 

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(QuizModel quizModel)
        {
            if (ModelState.IsValid)
            {
                /*bool addQuiz = repository.AddQuiz(quizModel);*/
                string url = "api/AdminAPI/AdminAPI";
                string content = JsonConvert.SerializeObject(quizModel);
                string res = await WebHelper.HttpClientPostRequest(url, content);
                bool addQuiz = JsonConvert.DeserializeObject<bool>(res);

                if (addQuiz)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(quizModel);
        }

        public async Task<ActionResult> AdminProfile(int adminId)
        {
            try
            {
                AdminModel adminModel = new AdminModel();
                /*adminModel = repository.GetAdminProfile(adminId);*/

                string url = $"api/AdminAPI/GetAdminProfile?adminId={adminId}";
                string res = await WebHelper.HttpRequestResponse(url);
                adminModel = JsonConvert.DeserializeObject<AdminModel>(res);
                if(adminModel != null  && adminModel.admin_id > 0)
                {
                    return View(adminModel);
                }

                return RedirectToAction("Index");
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AdminProfile(AdminModel adminModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    /*bool CheckUpdateProfile = repository.UpdateAdminProfile(adminModel);*/

                    string url = "api/AdminAPI/UpdateAdminProfile";
                    string content = JsonConvert.SerializeObject(adminModel);
                    string res = await WebHelper.HttpClientPostRequest(url, content);
                    bool CheckUpdateProfile = JsonConvert.DeserializeObject<bool>(res);

                    if (CheckUpdateProfile)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        
                        return View(adminModel);
                    }
                }
                else
                {
                    return View(adminModel);
                }
           
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ActionResult> EditQuiz(int quiz_id)
        {
            /*QuizModel quizModel = repository.GetQuizById(quiz_id);*/

            QuizModel quizModel = new QuizModel();
            string url = $"api/AdminAPI/GetQuizById?quiz_id={quiz_id}";
            string res = await WebHelper.HttpRequestResponse(url);
            quizModel = JsonConvert.DeserializeObject<QuizModel>(res);
            return View(quizModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditQuiz(QuizModel quizModel)
        {
            try
            {
                /* bool CheckUpdateOrNot = repository.UpdateQuizById(quizModel);*/
                if (ModelState.IsValid)
                {
                    string url = "api/AdminAPI/UpdateQuizById";
                    string content = JsonConvert.SerializeObject(quizModel);
                    string res = await WebHelper.HttpClientPostRequest(url, content);
                    bool CheckUpdateOrNot = JsonConvert.DeserializeObject<bool>(res);
                    if (CheckUpdateOrNot)
                    {
                        return RedirectToAction("Index");
                    }
                }

                return View(quizModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ActionResult> DeleteQuiz(int quiz_id)
        {
            try
            {
                /*bool deleteQuiz = repository.DeleteQuiz(quiz_id);*/
                string url = $"api/AdminAPI/DeleteQuiz?quiz_id={quiz_id}";
                string res = await WebHelper.HttpRequestResponse(url);
                bool deleteQuiz = JsonConvert.DeserializeObject<bool>(res);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}